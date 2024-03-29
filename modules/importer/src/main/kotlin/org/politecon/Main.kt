package org.politecon

import com.fasterxml.jackson.core.type.TypeReference
import com.fasterxml.jackson.databind.ObjectMapper
import com.fasterxml.jackson.dataformat.xml.XmlMapper
import com.fasterxml.jackson.dataformat.yaml.YAMLMapper
import com.fasterxml.jackson.module.kotlin.jacksonMapperBuilder
import com.fasterxml.jackson.module.kotlin.jacksonObjectMapper
import com.google.common.hash.Hashing
import com.neovisionaries.i18n.CountryCode
import io.ktor.client.*
import io.ktor.client.engine.cio.*
import kotlinx.coroutines.async
import kotlinx.coroutines.runBlocking
import mu.KotlinLogging
import org.apache.commons.codec.binary.StringUtils
import org.politecon.common.datamodel.Area
import org.politecon.common.datamodel.DataDimension
import org.politecon.common.datamodel.DataSubject
import org.politecon.common.datamodel.DataUnit
import org.politecon.common.datamodel.datapoint.SubjectDataPoint
import org.politecon.common.datamodel.datapoint.PopulationDataPoint
import org.politecon.sourceadapter.SdmxXmlRestClient
import org.politecon.sourceadapter.excel.ExcelLoader
import org.politecon.sourceadapter.fred.FredCsvLoader
import org.politecon.sourceadapter.oecd.OecdClient
import org.politecon.sourceadapter.oecd.OecdDataMapper
import org.politecon.sourceadapter.oecd.OecdDataSets
import org.politecon.sourceadapter.un.UnClient
import org.politecon.sourceadapter.un.UnCountryDataMapper
import org.politecon.sourceadapter.un.UnCsvLoader
import org.politecon.sourceadapter.un.UnEnergyDataMapper
import org.politecon.storage.db.DbCollection
import org.politecon.storage.db.Storage
import org.politecon.util.getResourceAsStream
import org.politecon.util.getResourceAsText
import org.politecon.util.getStreamFromZip
import org.yaml.snakeyaml.Yaml
import java.time.LocalDate
import kotlin.system.measureTimeMillis

private val logger = KotlinLogging.logger {}

/**
 * Начальная точка запуска
 *
 * TODO add DI
 * TODO добавить маппер в dataset
 * TODO Батч загрузчик
 * TODO корреляция между денежной массой и инфляцией
 */
fun main() {
    printBanner()


    val zipFilePath = "/data.zip"

    val http = HttpClient(CIO)
    val xmlMapper = XmlMapper()
    val objectMapper = jacksonObjectMapper()
    val yaml = YAMLMapper()
    val store = Storage(objectMapper, Hashing.murmur3_128())
    val sdmx = SdmxXmlRestClient(http, xmlMapper)

    val loader = ExcelLoader(objectMapper)
    val unEnergyClient = UnClient(sdmx, UnEnergyDataMapper())
    val unCountryClient = UnClient(sdmx, UnCountryDataMapper())

    val config = yaml.readTree(getResourceAsStream("/config/main.yaml"))

    logger.info { config }

    val startDate = LocalDate.of(1960, 1, 1)
    val endDate = LocalDate.of(2022, 12, 31)

    val elapsed = measureTimeMillis {
        runBlocking {
            val patents = loader.loadFile(getStreamFromZip(zipFilePath, "patents.xlsx"))
            store.storeDocuments(DbCollection.PATENTS, patents)

            val corporateFinances = loader.loadFile(getStreamFromZip(zipFilePath, "corp_fin.xlsx"))
            store.storeDocuments(DbCollection.CORPORATE_FINANCE, corporateFinances)

            val energyLoader = UnCsvLoader(getStreamFromZip(zipFilePath, "un/energy/coal.csv"), UnEnergyDataMapper())
            val records = energyLoader.read()
            store.store(DbCollection.COMMODITY, records)

            val m1 = FredCsvLoader(getStreamFromZip(zipFilePath, "fred/M1SL.csv")).read(
                CountryCode.US,
                DataSubject.M1,
                DataDimension.INDICATOR,
                DataUnit.NUMBER
            )
            store.store(DbCollection.ECONOMICS, m1)

            //******* Запрос данных **********
            val povertyJob = async {
                unCountryClient.fetchCountryData(
                    subjects = setOf(DataSubject.POPULATION_POVERTY),
                    areas = setOf(Area.ALL),
                    units = setOf(DataUnit.NUMBER, DataUnit.PERCENT),
                    startDate = startDate,
                    endDate = endDate
                )
            }

            val energyDataJob = async {
                unEnergyClient.fetchEnergyData(
                    countries = setOf(CountryCode.RU, CountryCode.US),
                    commodities = setOf(DataSubject.NATURAL_GAS, DataSubject.TOTAL_ELECTRICITY),
                    dataDimensions = setOf(DataDimension.PRODUCTION, DataDimension.IMPORT),
                    startDate = startDate,
                    endDate = endDate
                )
            }

            val finJob = async {
                val eocd = OecdClient(SdmxXmlRestClient(http, xmlMapper), OecdDataMapper())
                eocd.fetchFinancials(
                    datasetCode = OecdDataSets.FINANCIALS.datasetCode,
                    locations = setOf(CountryCode.US, CountryCode.CA),
                    subjects = setOf(DataSubject.M1, DataSubject.M3),
                    startDate = LocalDate.of(1960, 1, 1),
                    endDate = LocalDate.of(2022, 12, 31)
                )
            }

            val energyData = energyDataJob.await()
            val poverty = povertyJob.await()
            val fins = finJob.await()

            store.store(DbCollection.POPULATION, poverty)
            store.store(DbCollection.COMMODITY, energyData)
            store.store(DbCollection.ECONOMICS, fins)

            val subjectDataPoints:Set<SubjectDataPoint> = store.get(DbCollection.COMMODITY, typeRef())
            logger.info { subjectDataPoints.joinToString() }

            val popDataPoints: Set<PopulationDataPoint> = store.get(DbCollection.POPULATION, typeRef(), limit = 50)
            logger.info { popDataPoints.joinToString() }

        }
    }

    logger.info("Загрузка завершилась за $elapsed мс")
}

private fun printBanner() {
    logger.info("\n" + getResourceAsText("/banner.txt"))
}

inline fun <reified T> typeRef(): TypeReference<T> {
    return object : TypeReference<T>() {}
}

