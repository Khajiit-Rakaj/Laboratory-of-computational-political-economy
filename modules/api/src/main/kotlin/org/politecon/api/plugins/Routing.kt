package org.politecon.api.plugins

import com.fasterxml.jackson.module.kotlin.jacksonObjectMapper
import com.google.common.hash.Hashing
import io.ktor.http.*
import io.ktor.server.routing.*
import io.ktor.server.response.*
import io.ktor.server.http.content.*
import io.ktor.server.application.*
import org.politecon.api.models.EconomicsModel
import org.politecon.storage.db.Storage
import repository.EconomicsRepo

fun Application.configureRouting() {

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

        get("/economics") {
            val dataPoints = econimicsModel.GetEconomicsData()
            kotlin.runCatching {
                call.respondText(
                    objectMapper.writeValueAsString(dataPoints),
                    contentType
                )
            }
        }

        get("/tables") {
            kotlin.runCatching {
                call.respondText(
                    objectMapper.writeValueAsString(econimicsModel.GetTablesList()),
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
