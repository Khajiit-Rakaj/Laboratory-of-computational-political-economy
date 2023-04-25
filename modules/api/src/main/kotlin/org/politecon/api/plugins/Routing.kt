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
                    objectMapper.writeValueAsString(econimicsModel.GetTablesList()), contentType
                )
            }
        }

        get("/tables/data") {
            kotlin.runCatching {
                when (call.request.queryParameters["table"]) {
                    "economics" -> call.respondText(
                        objectMapper.writeValueAsString(econimicsModel.GetEconomicsData()), contentType
                    )

                    "patents" -> call.respondText(
                        objectMapper.writeValueAsString(econimicsModel.GetPatentsData()), contentType
                    )

                    "population_data" -> call.respondText(
                        objectMapper.writeValueAsString(econimicsModel.GetPopulationData()), contentType
                    )

                    "commodity_data" -> call.respondText(
                        objectMapper.writeValueAsString(econimicsModel.GetCommodityData()), contentType
                    )

                    "corporate_finance" -> call.respondText(
                        objectMapper.writeValueAsString(econimicsModel.GetCorporateFinanceData()), contentType
                    )
                }
            }
        }

        // Static plugin. Try to access `/static/index.html`
        static("/static") {
            resources("static")
        }
    }
}
