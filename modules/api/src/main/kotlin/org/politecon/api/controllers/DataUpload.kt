package org.politecon.api.controllers

import io.ktor.http.*
import io.ktor.http.content.*
import io.ktor.resources.*
import io.ktor.server.application.*
import io.ktor.server.request.*
import io.ktor.server.resources.*
import io.ktor.server.resources.post
import io.ktor.server.response.*
import io.ktor.server.routing.*
import io.ktor.utils.io.*
import kotlinx.serialization.Contextual
import org.politecon.api.Utils.HTTPUtils
import org.politecon.api.enums.FailResponses
import org.politecon.api.enums.SuccessfulResponses
import org.politecon.loaders.FileLoaderProvider

@Resource("/dataupload")
class DataUpload {
    @Resource("fromfile")
    class FromFile(val parent: DataUpload = DataUpload())

    @Resource("fromstring")
    class FromString(val parent: DataUpload = DataUpload())


    suspend fun fromFile(files: ArrayList<Pair<String, ByteArray>>, type: String, collection: String, scope: String): Boolean {
        val fileLoaderProvider: FileLoaderProvider = FileLoaderProvider()
        var result: Boolean = true

        files.forEach { file ->
            val loader = fileLoaderProvider.provide(file.first, type)

            result = if (loader != null) {
                result && loader.upload(file.second.inputStream(), collection)
            } else {
                false
            }
        }

        return result
    }
}

fun Application.module() {
    val dataUpload = DataUpload()

    routing {
        var resultCode: HttpStatusCode = HttpStatusCode.BadRequest
        var resultMessage: String = ""

        get<DataUpload> {
            call.respondText(FailResponses.OnlyPostMethodAvailable.response)
        }

        get<DataUpload.FromFile> {
            resultMessage = FailResponses.OnlyPostMethodAvailable.response
            call.respondText(resultMessage, status = resultCode)
        }

        post<DataUpload.FromFile> {
            val data: MultiPartData = call.receiveMultipart()
            val formData = HTTPUtils.getFormData(data)

            if (formData.fields.any(predicate = { it.first == "collection" && it.second.isNotEmpty() }) && formData.files.any()) {
                val collection = formData.fields.first(predicate = { it.first == "collection" }).second
                val type = formData.fields.first(predicate = { it.first == "type" }).second
                val scope = formData.fields.firstOrNull(predicate = { it.first == "scope" })?.second ?: "_default"

                if (dataUpload.fromFile(formData.files, type, collection, scope)) {
                    resultCode = HttpStatusCode.Created
                    resultMessage = SuccessfulResponses.Created.response
                } else {
                    resultMessage = FailResponses.FailToCreate.response
                }
            } else {
                resultMessage = FailResponses.FailToCreate.response
            }

            call.respondText(resultMessage, status = resultCode)
        }
        get<DataUpload.FromString> {

        }
    }
}
