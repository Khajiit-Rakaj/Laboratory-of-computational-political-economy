package org.politecon.api.controllers

import io.ktor.resources.*
import io.ktor.server.application.*
import io.ktor.server.resources.*
import io.ktor.server.response.*
import io.ktor.server.routing.*
import org.politecon.api.enums.FailResponses

@Resource("/dataupload")
class DataUpload {
}

fun Application.module() {

    routing {
        get<DataUpload> {
            call.respondText(FailResponses.OnlyPostMethodAvailable.response)
        }
    }
}
