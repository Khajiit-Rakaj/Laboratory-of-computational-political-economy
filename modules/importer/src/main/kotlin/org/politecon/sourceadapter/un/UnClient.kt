package org.politecon.sourceadapter.un

import com.fasterxml.jackson.databind.node.ArrayNode
import com.neovisionaries.i18n.CountryCode
import mu.KotlinLogging
import org.politecon.model.Area
import org.politecon.model.DataDimension
import org.politecon.model.DataSubject
import org.politecon.model.DataUnit
import org.politecon.model.datapoint.BaseDataPoint
import org.politecon.model.datapoint.DataPointValue
import org.politecon.model.datapoint.SubjectDataPoint
import org.politecon.model.datapoint.SubjectDimension
import org.politecon.model.datapoint.population.PopulationDataPoint
import org.politecon.sourceadapter.DataMapper
import org.politecon.sourceadapter.SdmxXmlRestClient
import org.politecon.util.childByAttribute
import java.time.LocalDate


private val logger = KotlinLogging.logger {}

/**
 * Http client for fetching data from UN
 *
 * TODO exception handling
 */
class UnClient(private val sdmx: SdmxXmlRestClient, private val dataMapper: DataMapper) {

    /**
     * Fetches data from UN Country Data Table
     */
    suspend fun fetchCountryData(
        subjects: Set<DataSubject>,
        areas: Set<Area>,
        units: Set<DataUnit>,
        startDate: LocalDate,
        endDate: LocalDate
    ): Set<PopulationDataPoint> {
        logger.info { "Достаём информацию по странам" }

        val frequencyParam = "A"
        val subjectParam = subjects.joinToString(separator = "+") { dataMapper.subjectToExternal(it) }
        val unitParam = units.joinToString(separator = "+") { it.name }
        val geo = areas.joinToString(separator = "+") { dataMapper.areaToExternal(it) }

        val request = sdmx.composeRequest(
            baseUrl = BASE_URL,
            pathSegments = listOf(
                UnDataSets.COUNTRY_DATA.datasetCode,
                "$frequencyParam.$subjectParam.$unitParam.$geo...."
            ),
            queryParameters = mapOf(
                "startPeriod" to startDate,
                "endPeriod" to endDate
            )
        )

        val tree = sdmx.execute(request)

        val result = mutableSetOf<PopulationDataPoint>()
        tree["DataSet"]["Series"].forEach { series ->
            val refData = series["SeriesKey"]["Value"]

            val area = dataMapper.areaFromExternal(childByAttribute(refData, "id", "LOCATION"))
            val countryCode = childByAttribute(refData, "id", "REF_AREA")
            val sex = dataMapper.sexFromExternal(childByAttribute(refData, "id", "SEX"))
            val unit = DataUnit.valueOf(childByAttribute(refData, "id", "UNIT"))
            val ageRangeTokens = childByAttribute(refData, "id", "AGE_GROUP").split("_")
            val subjectCode = childByAttribute(refData, "id", "SERIES")

            // Если нода одна, её нужно завернуть в массив
            val obs = when (val obsNode = series["Obs"]) {
                is ArrayNode -> obsNode
                else -> listOf(obsNode)
            }

            obs.forEach {
                val dateValue = it["ObsDimension"]["value"].textValue()

                val dataPoint: PopulationDataPoint = BaseDataPoint.create {
                    date = dateValue
                    country = CountryCode.getByCode(countryCode)
                    areaConstraint = area
                    sexConstraint = sex
                    ageConstraint = IntRange(ageRangeTokens[0].toInt(), ageRangeTokens[1].toInt())
                    value = DataPointValue(it["ObsValue"]["value"].textValue().toDouble(), unit)
                    source = SOURCE_NAME
                    subject = dataMapper.subjectFromExternal(subjectCode)
                }

                result.add(dataPoint)
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

        val frequencyParam = "A"
        val countryParams = countries.joinToString(separator = "+") { dataMapper.countryToExternal(it) }
        val subjectParams = commodities.joinToString(separator = "+") { dataMapper.subjectToExternal(it) }
        val dimensionParams = dataDimensions.joinToString(separator = "+") { dataMapper.dimensionToExternal(it) }

        val request = sdmx.composeRequest(
            baseUrl = BASE_URL,
            pathSegments = listOf(
                UnDataSets.ENERGY.datasetCode,
                "$frequencyParam.$countryParams.$subjectParams.$dimensionParams"
            ),
            queryParameters = mapOf(
                "startPeriod" to startDate,
                "endPeriod" to endDate
            )
        )

        val tree = sdmx.execute(request)

        val result = mutableSetOf<SubjectDataPoint>()
        tree["DataSet"]["Series"].forEach { series ->
            val refData = series["SeriesKey"]["Value"]

            val countryCode = childByAttribute(refData, "id", "REF_AREA")
            val subjectCode = childByAttribute(refData, "id", "COMMODITY")
            val dimensionCode = childByAttribute(refData, "id", "TRANSACTION")

            val unit = dataMapper.unitFromExternal(series["Attributes"]["Value"]["value"].textValue())

            series["Obs"].forEach {
                val subjectDataPoint: SubjectDataPoint = BaseDataPoint.create {
                    source = SOURCE_NAME
                    country = dataMapper.countryFromExternal(countryCode)
                    subjectDimension = SubjectDimension(
                        subject = dataMapper.subjectFromExternal(subjectCode),
                        dimension = dataMapper.dimensionFromExternal(dimensionCode)
                    )
                    value = DataPointValue(it["ObsValue"]["value"].textValue().toDouble(), unit)
                    date = it["ObsDimension"]["value"].textValue()
                }

                result.add(subjectDataPoint)
            }

        }

        return result
    }

    companion object {
        private const val SOURCE_NAME = "UN"
        private const val BASE_URL = "https://data.un.org/ws/rest/data/"
    }
}