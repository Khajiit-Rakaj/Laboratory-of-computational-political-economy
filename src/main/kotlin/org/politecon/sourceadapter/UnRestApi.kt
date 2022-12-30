package org.politecon.sourceadapter

import com.fasterxml.jackson.dataformat.xml.XmlMapper
import io.ktor.client.*
import io.ktor.client.request.*
import io.ktor.client.statement.*
import mu.KotlinLogging
import org.politecon.model.Commodity
import org.politecon.model.Country
import org.politecon.model.DataDimension
import org.politecon.model.Units
import org.politecon.model.datapoint.CommodityDataPoint
import org.politecon.model.datapoint.CommodityDimension
import org.politecon.model.datapoint.DataPointValue
import org.politecon.util.Substitutions
import java.time.LocalDate
import kotlin.system.measureNanoTime
import kotlin.system.measureTimeMillis


private val logger = KotlinLogging.logger {}

/**
 * Http client for fetching data from UN
 *
 * @param http initialized http client
 * @param mapper configured deserializer
 *
 * TODO exception handling
 * TODO conversion to recognized units (i.e. joules to M3 for NG)
 */
class UnRestApi(private val http: HttpClient, private val mapper: XmlMapper) {

    suspend fun fetchCommodity(
        countries: Set<Country>,
        commodities: Set<Commodity>,
        dataDimensions: Set<DataDimension>,
        startDate: LocalDate,
        endDate: LocalDate
    ): Set<CommodityDataPoint> {
        val url = Substitutions.makeString(
            URL_TEMPLATE,
            mapOf(
                COUNTRY_TOKEN to countries.map { COUNTRY_MAP[it] }.joinToString(separator = LIST_SEPARATOR),
                SUBJECT_TOKEN to commodities.map { SUBJECT_MAP[it] }.joinToString(separator = LIST_SEPARATOR),
                DATA_DIMENSION_TOKEN to dataDimensions.map { DATA_DIMENSION_MAP[it] }
                    .joinToString(separator = LIST_SEPARATOR),
                START_DATE_TOKEN to startDate,
                END_DATE_TOKEN to endDate
            )
        )

        val response: HttpResponse
        val apiTime = measureTimeMillis {
            logger.info("Fetching from external url $url")
            response = http.get(url)
        }

        logger.info { "Call finished with status ${response.status} in $apiTime ms" }

        val tree = mapper.readTree(response.bodyAsText())

        val result = mutableSetOf<CommodityDataPoint>()

        tree["DataSet"]["Series"].forEach { series ->
            val refData = series["SeriesKey"]["Value"]

            val countryCode = refData.first { it["id"].textValue() == "REF_AREA" }["value"].textValue()
            val subjectCode = refData.first { it["id"].textValue() == "COMMODITY" }["value"].textValue()
            val dimensionCode = refData.first { it["id"].textValue() == "TRANSACTION" }["value"].textValue()

            val unitCode = series["Attributes"]["Value"]["value"].textValue()

            series["Obs"].forEach {

                val dataValue = UNIT_MAPPER[unitCode]?.invoke(it["ObsValue"]["value"].textValue())
                val date = it["ObsDimension"]["value"].textValue()

                val dataPoint = CommodityDataPoint.create {
                    source = SOURCE_NAME
                    country = REVERSE_COUNTRY_MAP[countryCode]!!
                    commodityDimension = CommodityDimension(commodity = REVERSE_SUBJECT_MAP[subjectCode]!!, dimension = REVERSE_DATA_DIMENSION_MAP[dimensionCode]!!)
                    value = dataValue!!
                    timeFrame = date
                }

                // TODO validate data point
                result.add(dataPoint)
            }

        }

        return result
    }

    companion object {

        private const val SOURCE_NAME = "UN"

        private const val COUNTRY_TOKEN = "countries"
        private const val SUBJECT_TOKEN = "subjects"
        private const val DATA_DIMENSION_TOKEN = "data_dimension"

        private const val START_DATE_TOKEN = "startDate"
        private const val END_DATE_TOKEN = "endDate"

        private const val LIST_SEPARATOR = "+"

        private const val URL_TEMPLATE =
            "https://data.un.org/ws/rest/data/UNSD,DF_UNDATA_ENERGY,1.0/.#{$COUNTRY_TOKEN}.#{$SUBJECT_TOKEN}.#{$DATA_DIMENSION_TOKEN}/ALL/?detail=full&startPeriod=#{$START_DATE_TOKEN}&endPeriod=#{$END_DATE_TOKEN}&dimensionAtObservation=TIME_PERIOD"

        /**
         * Mappings for countries (i.e. Russia)
         */
        private val COUNTRY_MAP = mapOf(
            Country.RUSSIA to "643",
            Country.USA to "840"
        )

        private val REVERSE_COUNTRY_MAP = COUNTRY_MAP.entries.associate { (key, value) -> value to key }

        /**
         * Mappings for subjects (i.e. commodities)
         */
        private val SUBJECT_MAP = mapOf(
            Commodity.NATURAL_GAS to "3000"
        )

        private val REVERSE_SUBJECT_MAP = SUBJECT_MAP.entries.associate { (key, value) -> value to key }

        /**
         * Mappings for data dimensions (i.e. production)
         */
        private val DATA_DIMENSION_MAP = mapOf(
            DataDimension.PRODUCTION to "01",
            DataDimension.IMPORT to "03",
            DataDimension.EXPORT to "04"
        )

        private val REVERSE_DATA_DIMENSION_MAP = DATA_DIMENSION_MAP.entries.associate { (key, value) -> value to key }

        private val UNIT_MAPPER= mapOf(
            "TJ" to { value: String -> DataPointValue(value.toDouble() * 1000000000000, Units.JOULES) }
        )
    }
}