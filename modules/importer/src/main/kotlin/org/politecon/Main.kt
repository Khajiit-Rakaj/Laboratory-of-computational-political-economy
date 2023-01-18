package org.politecon

import com.fasterxml.jackson.databind.ObjectMapper
import com.fasterxml.jackson.dataformat.xml.XmlMapper
import com.google.common.hash.Hashing
import com.neovisionaries.i18n.CountryCode
import io.ktor.client.*
import io.ktor.client.engine.cio.*
import kotlinx.coroutines.async
import kotlinx.coroutines.runBlocking
import mu.KotlinLogging
import org.politecon.model.Area
import org.politecon.model.DataDimension
import org.politecon.model.DataSubject
import org.politecon.model.DataUnit
import org.politecon.model.datapoint.SubjectDataPoint
import org.politecon.model.datapoint.population.PopulationDataPoint
import org.politecon.persist.DbCollection
import org.politecon.persist.Storage
import org.politecon.sourceadapter.eocd.EocdClient
import org.politecon.sourceadapter.eocd.EocdDataSets
import org.politecon.sourceadapter.excel.ExcelLoader
import org.politecon.sourceadapter.un.UnCsvLoader
import org.politecon.sourceadapter.un.UnEnergyDataMapper
import org.politecon.sourceadapter.un.UnRestApi
import org.politecon.util.getResourceAsText
import java.time.LocalDate
import kotlin.system.measureTimeMillis

private val logger = KotlinLogging.logger {}

/**
 * Starting point for application
 *
 * TODO add DI
 * TODO корреляция между денежной массой и инфляцией
 */
fun main() {
    printBanner()

    val http = HttpClient(CIO)
    val xmlMapper = XmlMapper()
    val objectMapper = ObjectMapper()
    val store = Storage(objectMapper, Hashing.murmur3_128())

    val loader = ExcelLoader(objectMapper)
    val unRestApi = UnRestApi(http = http, mapper = xmlMapper)

    val energyLoader = UnCsvLoader("/csv/un/energy/coal.csv", UnEnergyDataMapper())

    val startDate = LocalDate.of(1900, 1, 1)
    val endDate = LocalDate.of(2022, 12, 31)

    val elapsed = measureTimeMillis {
        runBlocking {

            val corporateFinances = loader.loadFile("/corporate_finance.xlsx")
            store.storeDocuments(DbCollection.CORPORATE_FINANCE, corporateFinances)

            val records = energyLoader.read()
            store.store(DbCollection.COMMODITY, records)


            val eocd = EocdClient(http, objectMapper)
            val m1 = eocd.fetchFinancials(
                datasetCode = EocdDataSets.FINANCIALS.datasetCode,
                location = CountryCode.US,
                subject = DataSubject.M1,
                startDate = LocalDate.of(1960, 1,1),
                endDate = LocalDate.of(2022,12,31)
            )

            logger.info {m1.joinToString(separator = "\n")}

            //******* Запрос данных **********
            val povertyJob = async {
                unRestApi.fetchCountryData(
                    subjects = setOf(DataSubject.POPULATION_POVERTY),
                    geoDiscriminators = setOf(Area.ALL),
                    units = setOf(DataUnit.NUMBER, DataUnit.PERCENT),
                    startDate = startDate,
                    endDate = endDate
                )
            }

            val energyDataJob = async {
                unRestApi.fetchEnergyData(
                    countries = setOf(CountryCode.RU, CountryCode.US),
                    commodities = setOf(DataSubject.NATURAL_GAS, DataSubject.ELECTRICITY),
                    dataDimensions = setOf(DataDimension.PRODUCTION, DataDimension.IMPORT),
                    startDate = startDate,
                    endDate = endDate
                )
            }

            val energyData = energyDataJob.await()
            val poverty = povertyJob.await()

            store.store(DbCollection.POPULATION, poverty)
            store.store(DbCollection.COMMODITY, energyData)

            val subjectDataPoints: Set<SubjectDataPoint> = store.get(DbCollection.COMMODITY)
            logger.info { subjectDataPoints.joinToString() }

            val popDataPoints: Set<PopulationDataPoint> = store.get(DbCollection.POPULATION, limit = 50)
            logger.info { popDataPoints.joinToString() }

        }
    }

    logger.info("Application finished in $elapsed ms")
}

private fun printBanner() {
    logger.info("\n" + getResourceAsText("/banner.txt"))
}



