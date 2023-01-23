package org.politecon.sourceadapter.oecd

import com.fasterxml.jackson.databind.JsonNode
import com.fasterxml.jackson.dataformat.xml.XmlMapper
import com.neovisionaries.i18n.CountryCode
import io.ktor.client.*
import io.ktor.client.engine.cio.*
import mu.KotlinLogging
import org.politecon.model.DataDimension
import org.politecon.model.DataSubject
import org.politecon.model.datapoint.BaseDataPoint
import org.politecon.model.datapoint.DataPointValue
import org.politecon.model.datapoint.SubjectDataPoint
import org.politecon.model.datapoint.SubjectDimension
import org.politecon.sourceadapter.DataMapper
import org.politecon.sourceadapter.SdmxXmlRestClient
import org.politecon.util.childByAttribute
import java.time.LocalDate

private val logger = KotlinLogging.logger {}

/**
 * OECD
 */
class OecdClient(private val sdmx: SdmxXmlRestClient, private val dataMapper: DataMapper) {

    suspend fun fetchFinancials(
        datasetCode: String,
        locations: Set<CountryCode>,
        subjects: Set<DataSubject>,
        startDate: LocalDate, endDate: LocalDate
    ): MutableSet<SubjectDataPoint> {

        val locationString = locations.joinToString(separator = "+") { dataMapper.countryToExternal(it) }
        val subjectString = subjects.joinToString(separator = "+") { dataMapper.subjectToExternal(it) }

        val request = sdmx.composeRequest(
            baseUrl = BASE_URL,
            pathSegments = listOf(datasetCode, "$subjectString.$locationString.A"),
            queryParameters = mapOf(
                "startTime" to startDate,
                "endTime" to endDate
            )
        )

        val tree = sdmx.execute(request)

        val dataPoints = mutableSetOf<SubjectDataPoint>()
        tree["DataSet"]["Series"].forEach { series ->
            val ref = series["SeriesKey"]["Value"]

            val country = dataMapper.countryFromExternal(childByAttribute(ref, "concept", "LOCATION"))
            val subject = dataMapper.subjectFromExternal(childByAttribute(ref, "concept", "SUBJECT"))
            val unit = dataMapper.unitFromExternal(childByAttribute(series["Attributes"]["Value"], "concept", "UNIT"))

            series["Obs"].forEach {
                val dataPoint: SubjectDataPoint = BaseDataPoint.create {
                    this.source = SOURCE
                    this.country = country
                    this.subjectDimension = SubjectDimension(subject, DataDimension.INDICATOR)
                    this.value = DataPointValue(it["ObsValue"]["value"].textValue().toDouble(), unit)
                    this.date = it["Time"].textValue()
                }

                dataPoints.add(dataPoint)
            }
        }

        return dataPoints
    }


    companion object {
        private const val BASE_URL = "https://stats.oecd.org/restsdmx/sdmx.ashx/GetData"
        private const val SOURCE = "EOCD"
    }
}