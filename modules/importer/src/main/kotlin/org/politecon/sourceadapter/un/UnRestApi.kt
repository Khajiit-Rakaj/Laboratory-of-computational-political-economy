package org.politecon.sourceadapter.un

import com.fasterxml.jackson.databind.JsonNode
import com.fasterxml.jackson.databind.node.ArrayNode
import com.fasterxml.jackson.dataformat.xml.XmlMapper
import com.neovisionaries.i18n.CountryCode
import io.ktor.client.*
import io.ktor.client.request.*
import io.ktor.client.statement.*
import mu.KotlinLogging
import org.politecon.model.*
import org.politecon.model.datapoint.BaseDataPoint
import org.politecon.model.datapoint.DataPointValue
import org.politecon.model.datapoint.SubjectDataPoint
import org.politecon.model.datapoint.SubjectDimension
import org.politecon.model.datapoint.population.PopulationDataPoint
import org.politecon.util.Substitutions
import java.time.LocalDate
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

    /**
     * Fetches data from UN Country Data Table
     */
    suspend fun fetchCountryData(
        subjects: Set<DataSubject>,
        geoDiscriminators: Set<Area>,
        units: Set<Units>,
        startDate: LocalDate,
        endDate: LocalDate
    ): Set<PopulationDataPoint> {

        val tableName = "UNSD,DF_UNDATA_COUNTRYDATA,1.4"

        val frequencyParam = "A"
        val subjectParam = subjects.map { SUBJECT_MAP[it] }.joinToString(separator = LIST_SEPARATOR)
        val unitParam = units.joinToString(separator = LIST_SEPARATOR) { it.name }
        val geo = geoDiscriminators.map { AREA_MAP[it] }.joinToString(separator = LIST_SEPARATOR)

        val params = "$frequencyParam.$subjectParam.$unitParam.$geo...."

        val url = assembleUrl(
            table = tableName,
            params = params,
            startDate = startDate,
            endDate = endDate
        )

        val tree = callUrl(url)

        val result = mutableSetOf<PopulationDataPoint>()
        if (tree == null) {
            logger.error { "Failed to parse xml into tree" }
        } else {
            tree["DataSet"]["Series"].forEach { series ->
                val refData = series["SeriesKey"]["Value"]

                val area = REVERSE_AREA_MAP[getNodeValue(refData, "LOCATION")] ?: Area.UNKNOWN
                val countryCode = getNodeValue(refData, "REF_AREA")
                val sex = REVERSE_SEX_MAP[getNodeValue(refData, "SEX")] ?: Sex.UNKNOWN
                val unit = Units.valueOf(getNodeValue(refData, "UNIT"))
                val ageRangeTokens = getNodeValue(refData, "AGE_GROUP").split("_")
                val subjectCode = getNodeValue(refData, "SERIES")

                // Если нода одна, её нужно завернуть в массив
                val obs = when (val obsNode = series["Obs"]) {
                    is ArrayNode -> obsNode
                    else -> {
                        val wrapper = this.mapper.createArrayNode()
                        wrapper.add(obsNode)
                    }
                }

                obs.forEach {
                    val dateValue = it["ObsDimension"]["value"].textValue()

                    val dataPoint: PopulationDataPoint = BaseDataPoint.create {
                        date = dateValue
                        country = CountryCode.getByCode(countryCode)
                        areaConstraint = area
                        sexConstraint = sex
                        ageConstraint = IntRange(ageRangeTokens[0].toInt(), ageRangeTokens[1].toInt())
                        value = DataPointValue(value = it["ObsValue"]["value"].textValue().toDouble(), unit)
                        source = "UN-$tableName-${getNodeValue(it["Attributes"]["Value"], "SOURCE_DETAIL")}"
                        subject = REVERSE_SUBJECT_MAP[subjectCode] ?: DataSubject.UNKNOWN
                    }

                    result.add(dataPoint)
                }
            }
        }

        return result
    }

    /**
     * Fetches Energy Data
     */
    suspend fun fetchEnergyData(
        countries: Set<CountryCode>,
        commodities: Set<DataSubject>,
        dataDimensions: Set<DataDimension>,
        startDate: LocalDate,
        endDate: LocalDate
    ): Set<SubjectDataPoint> {

        val tableName = "UNSD,DF_UNDATA_ENERGY,1.0"

        val frequencyParam = "A"
        val countryParams = countries.map { COUNTRY_MAP[it] }.joinToString(separator = LIST_SEPARATOR)
        val subjectParams = commodities.map { SUBJECT_MAP[it] }.joinToString(separator = LIST_SEPARATOR)
        val dimensionParams = dataDimensions.map { DATA_DIMENSION_MAP[it] }.joinToString(separator = LIST_SEPARATOR)

        val params = "$frequencyParam.$countryParams.$subjectParams.$dimensionParams"

        val url = assembleUrl(
            table = tableName,
            params = params,
            startDate = startDate,
            endDate = endDate
        )

        val tree = callUrl(url)

        val result = mutableSetOf<SubjectDataPoint>()
        if (tree == null) {
            logger.error { "Failed to parse xml into tree" }
        } else {

            tree["DataSet"]["Series"].forEach { series ->
                val refData = series["SeriesKey"]["Value"]

                val countryCode = getNodeValue(refData, "REF_AREA")
                val subjectCode = getNodeValue(refData, "COMMODITY")
                val dimensionCode = getNodeValue(refData, "TRANSACTION")

                val unitCode = series["Attributes"]["Value"]["value"].textValue()

                series["Obs"].forEach {

                    val dataValue = UNIT_MAPPER[unitCode]?.invoke(it["ObsValue"]["value"].textValue())
                    val dateValue = it["ObsDimension"]["value"].textValue()

                    val subjectDataPoint: SubjectDataPoint = BaseDataPoint.create {
                        source = SOURCE_NAME
                        country = REVERSE_COUNTRY_MAP[countryCode]!!
                        subjectDimension = SubjectDimension(
                            subject = REVERSE_SUBJECT_MAP[subjectCode]!!,
                            dimension = REVERSE_DATA_DIMENSION_MAP[dimensionCode]!!
                        )
                        value = dataValue!!
                        date = dateValue
                    }

                    result.add(subjectDataPoint)
                }

            }
        }

        return result
    }

    private suspend fun callUrl(url: String): JsonNode? {
        val response: HttpResponse
        val apiTime = measureTimeMillis {
            logger.info("Fetching from external url $url")
            response = http.get(url)
        }

        logger.info { "Call finished with status ${response.status} in $apiTime ms" }
        logger.debug("Response: ${response.bodyAsText()}")

        // Компилятор ругается если вызывается не корутина способная кидать исключения
        return runCatching { mapper.readTree(response.bodyAsText()) }.getOrNull()
    }

    private fun assembleUrl(
        table: String,
        params: String,
        startDate: LocalDate,
        endDate: LocalDate
    ): String {
        return Substitutions.makeString(
            URL_TEMPLATE, mapOf(
                UN_TABLE_TOKEN to table,
                PARAM_TOKEN to params,
                START_DATE_TOKEN to startDate,
                END_DATE_TOKEN to endDate
            )
        )
    }

    private fun getNodeValue(refData: JsonNode, id: String): String {
        val filtered = refData.filter { it["id"].textValue() == id }

        val result = if (filtered.isNotEmpty()) {
            filtered.first()["value"].textValue()
        } else {
            logger.info { "Поиск ноды с айди $id провалился полностью" }
            "UNDEFINED"
        }

        return result
    }

    companion object {

        private const val SOURCE_NAME = "UN"

        private const val UN_TABLE_TOKEN = "un_table"

        private const val PARAM_TOKEN = "params"

        private const val START_DATE_TOKEN = "startDate"
        private const val END_DATE_TOKEN = "endDate"

        private const val LIST_SEPARATOR = "+"

        private const val URL_TEMPLATE =
            "https://data.un.org/ws/rest/data/#{$UN_TABLE_TOKEN}/#{$PARAM_TOKEN}/ALL/?detail=full&dimensionAtObservation=TIME_PERIOD&startPeriod=#{$START_DATE_TOKEN}&endPeriod=#{$END_DATE_TOKEN}"

        /**
         * Mappings for countries
         */
        private val COUNTRY_MAP = mapOf(
            CountryCode.RU to "643",
            CountryCode.US to "840"
        )

        private val REVERSE_COUNTRY_MAP = reverseAssociations(COUNTRY_MAP)

        /**
         * Mappings for subjects (i.e. commodities)
         */
        private val SUBJECT_MAP = mapOf(
            DataSubject.NATURAL_GAS to "3000",
            DataSubject.ELECTRICITY to "7000",
            DataSubject.POPULATION_POVERTY to "SI_POV_NAHC"
        )

        private val REVERSE_SUBJECT_MAP = reverseAssociations(SUBJECT_MAP)

        /**
         * Mappings for data dimensions (i.e. production)
         */
        private val DATA_DIMENSION_MAP = mapOf(
            DataDimension.PRODUCTION to "01",
            DataDimension.IMPORT to "03",
            DataDimension.EXPORT to "04"
        )

        private val REVERSE_DATA_DIMENSION_MAP = reverseAssociations(DATA_DIMENSION_MAP)

        private val AREA_MAP = mapOf(
            Area.ALL to "T",
            Area.RURAL to "R",
            Area.URBAN to "U"
        )

        private val REVERSE_AREA_MAP = reverseAssociations(AREA_MAP)

        private val SEX_MAP = mapOf(
            Sex.ALL to "T",
            Sex.FEMALE to "F",
            Sex.MALE to "M"
        )

        private val REVERSE_SEX_MAP = reverseAssociations(SEX_MAP)

        private val UNIT_MAPPER = mapOf(
            "TJ" to { value: String -> DataPointValue(value.toDouble() * 1000000000000, Units.JOULE) },
            "GWHR" to {value:String -> DataPointValue(value.toDouble()*1000000000, Units.WATT_HOUR)}
        )

        /**
         * Переворачивает направление ассоциации
         */
        private fun <K, V> reverseAssociations(map: Map<K, V>): Map<V, K> =
            map.entries.associate { (key, value) -> value to key }
    }
}