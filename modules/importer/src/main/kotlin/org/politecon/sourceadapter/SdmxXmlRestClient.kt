package org.politecon.sourceadapter

import com.fasterxml.jackson.databind.JsonNode
import com.fasterxml.jackson.dataformat.xml.XmlMapper
import io.ktor.client.*
import io.ktor.client.request.*
import io.ktor.client.statement.*
import io.ktor.http.*
import mu.KotlinLogging
import kotlin.system.measureTimeMillis

private val logger = KotlinLogging.logger {}

/**
 * Сервисы для вызова SDMX источников
 *
 * @param http Инициализированный http клиент
 * @param xml Инициализированный xml парсер
 */
class SdmxXmlRestClient(private val http: HttpClient, private val xml: XmlMapper) {

    /**
     * Создаёт хттп запрос
     */
    suspend fun composeRequest(
        baseUrl: String,
        pathSegments: List<String>,
        queryParameters: Map<String, *>
    ): HttpStatement {

        val request = http.prepareRequest(baseUrl) {
            url {
                appendPathSegments(pathSegments)
                queryParameters.forEach { (key, value) -> parameters.append(key, value.toString()) }
            }
        }

        return request
    }

    /**
     * Вызывает внешний ресурс
     *
     * @return полученные данные в нетипизированом древе
     */
    suspend fun execute(request: HttpStatement): JsonNode {
        val response: HttpResponse
        val apiTime = measureTimeMillis {
            logger.info("Запрос с внешнего УКР $request")
            response = request.execute()
        }

        logger.info { "Запрос завершён ${response.status} за $apiTime мс" }

        // bodyAsText взрывается из-за незнакомого Content-type
        val body = response.bodyAsText()
        logger.debug("Ответ: $body")

        // Компилятор ругается если вызывается не корутина способная кидать исключения
        return runCatching { xml.readTree(body) }.getOrNull() ?: xml.createObjectNode()
    }
}