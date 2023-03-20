package org.politecon.api.plugins

import com.fasterxml.jackson.core.type.TypeReference
import com.fasterxml.jackson.module.kotlin.jacksonObjectMapper
import com.google.common.hash.Hashing
import io.ktor.server.routing.*
import io.ktor.server.response.*
import io.ktor.server.http.content.*
import io.ktor.server.application.*
import org.politecon.api.models.tableListToModel
import org.politecon.common.datamodel.datapoint.SubjectDataPoint
import org.politecon.storage.db.DbCollection
import org.politecon.storage.db.Storage

fun Application.configureRouting() {

    val objectMapper = jacksonObjectMapper()
    val hashing = Hashing.murmur3_128()
    val storage = Storage(objectMapper, hashing)

    routing {
        get("/") {
            call.respondText("Hello World!")
        }

        get("/economics") {
            val dataPoints =
                storage.get(DbCollection.COMMODITY, object : TypeReference<SubjectDataPoint>() {}, limit = 50)
            kotlin.runCatching {
                call.respond(objectMapper.writeValueAsString(dataPoints))
            }
        }

        get("/tables") {
            val data = storage.getTablesList()
            val response = tableListToModel(data)
            kotlin.runCatching {
                call.respond(response)
            }
        }

        // Static plugin. Try to access `/static/index.html`
        static("/static") {
            resources("static")
        }
    }
}
