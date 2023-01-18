package org.politecon.sourceadapter.eocd

import com.fasterxml.jackson.databind.ObjectMapper
import com.neovisionaries.i18n.CountryCode
import io.ktor.client.*
import io.ktor.client.request.*
import io.ktor.client.statement.*
import io.ktor.http.*
import mu.KotlinLogging
import org.politecon.model.DataDimension
import org.politecon.model.DataSubject
import org.politecon.model.DataUnit
import org.politecon.model.datapoint.BaseDataPoint
import org.politecon.model.datapoint.DataPointValue
import org.politecon.model.datapoint.SubjectDataPoint
import org.politecon.model.datapoint.SubjectDimension
import java.time.LocalDate

private val logger = KotlinLogging.logger {}

/**
 * EOCD
 */
class EocdClient(
    private val http: HttpClient,
    private val mapper: ObjectMapper
) {

    suspend fun fetchFinancials(
        datasetCode: String,
        location: CountryCode,
        subject: DataSubject,
        startDate: LocalDate, endDate: LocalDate
    ): MutableSet<SubjectDataPoint> {

        val locationString = location.alpha3
        val subjectString = SUBJECT_MAP[subject]
        val request = composeRequest(
            listOf(datasetCode, "$subjectString.$locationString.A"), mapOf(
                "startTime" to startDate,
                "endTime" to endDate
            )
        )

        logger.info { "Вызывается $request" }
        val response = request.execute()

        // bodyAsText взрывается из-за незнакомого Content-type
        val body = String(response.readBytes())
        val docs = runCatching { mapper.readTree(body) }.getOrNull() ?: mapper.createObjectNode()

        val incomingData = docs["dataSets"][0]["series"]["0:0:0"]["observations"]

        val dataPoints = mutableSetOf<SubjectDataPoint>()
        incomingData.forEachIndexed { index, jsonNode ->
            val dataPoint = BaseDataPoint.create<SubjectDataPoint> {
                subjectDimension = SubjectDimension(subject, DataDimension.INDEX_YEAR)
                source = "OECD"
                country = location
                value = DataPointValue(jsonNode[0].doubleValue(), DataUnit.NUMBER)
                date = "${startDate.year + index}"
            }
            dataPoints.add(dataPoint)
        }

        return dataPoints
    }

    private suspend fun composeRequest(pathSegments: List<String>, queryParameters: Map<String, *>): HttpStatement {

        val request = http.prepareRequest(BASE_URL) {
            url {
                appendPathSegments(pathSegments)
                queryParameters.forEach { (key, value) -> parameters.append(key, value.toString()) }
            }
        }

        return request
    }

    companion object {

        private const val BASE_URL = "https://stats.oecd.org/SDMX-JSON/data/"

        private val SUBJECT_MAP = mapOf(
            DataSubject.M1 to "MANM",
            DataSubject.M3 to "MABM"
        )
    }
}