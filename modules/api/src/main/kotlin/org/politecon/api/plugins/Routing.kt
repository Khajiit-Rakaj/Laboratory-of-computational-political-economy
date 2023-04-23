package org.politecon.api.plugins

import com.fasterxml.jackson.module.kotlin.jacksonObjectMapper
import com.google.common.hash.Hashing
import io.ktor.http.*
import io.ktor.server.application.*
import org.politecon.api.models.EconomicsModel
import io.ktor.server.http.content.*
import io.ktor.server.resources.*
import io.ktor.server.response.*
import io.ktor.server.routing.*
import org.politecon.storage.db.Storage
import repository.EconomicsRepo

fun Application.configureRouting() {
    install(Resources)

    val objectMapper = jacksonObjectMapper()
    val hashing = Hashing.murmur3_128()
    val storage = Storage(objectMapper, hashing)
    val contentType = ContentType.Application.Json.withParameter("charset", "utf-8")

    val economicsRepo = EconomicsRepo(storage)
    val econimicsModel = EconomicsModel(economicsRepo)

    routing {
        get("/") {
            call.respondText("Hello World!")
        }

        get("/tables") {
            kotlin.runCatching {
                call.respondText(
                    objectMapper.writeValueAsString(econimicsModel.GetTablesList()),
                    contentType
                )
            }
        }

        get("/tables/economics") {
            kotlin.runCatching {
                val dataPoints = econimicsModel.GetEconomicsData()
                call.respondText(
                    objectMapper.writeValueAsString(dataPoints),
                    contentType
                )
            }
        }

        get("/tables/population_data") {
            kotlin.runCatching {
                val dataPoints = econimicsModel.GetPopulationData()
                call.respondText(
                    objectMapper.writeValueAsString(dataPoints),
                    contentType
                )
            }
        }

        get("/tables/patents") {
            kotlin.runCatching {
                val dataPoints = econimicsModel.GetPatentsData()
                call.respondText(
                    objectMapper.writeValueAsString(dataPoints),
                    contentType
                )
            }
        }

        get("/tables/commodity_data") {
            kotlin.runCatching {
                val dataPoints = econimicsModel.GetCommodityData()
                call.respondText(
                    objectMapper.writeValueAsString(dataPoints),
                    contentType
                )
            }
        }

        get("/tables/corporate_finance") {
            kotlin.runCatching {
                val dataPoints = econimicsModel.GetCorporateFinanceData()
                call.respondText(
                    objectMapper.writeValueAsString(dataPoints),
                    contentType
                )
            }
        }

        // Static plugin. Try to access `/static/index.html`
        static("/static") {
            resources("static")
        }
    }
}
