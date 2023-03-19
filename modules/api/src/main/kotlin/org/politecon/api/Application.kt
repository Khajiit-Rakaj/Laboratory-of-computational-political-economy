package org.politecon.api

import io.ktor.server.application.*
import org.politecon.api.plugins.configureHTTP
import org.politecon.api.plugins.configureRouting
import org.politecon.api.plugins.configureSerialization

fun main(args: Array<String>): Unit = io.ktor.server.netty.EngineMain.main(args)

fun Application.module() {
    configureHTTP()
    configureSerialization()
    configureRouting()
}
