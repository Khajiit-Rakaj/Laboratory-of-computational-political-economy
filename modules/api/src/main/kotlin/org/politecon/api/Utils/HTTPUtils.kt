package org.politecon.api.Utils

import io.ktor.http.content.*
import io.ktor.server.application.*
import io.ktor.server.request.*

class HTTPUtils {
    companion object {
        suspend fun getFormData(data: MultiPartData): FormData {
            val formData = FormData.create()

            data.forEachPart { partData ->
                when (partData) {
                    is PartData.FormItem -> {
                        formData.fields.add(Pair(partData.name ?: "", partData.value))
                    }

                    is PartData.FileItem -> {
                        val fileName = partData.originalFileName as String
                        val fileBytes: ByteArray = partData.streamProvider().readBytes()
                        formData.files.add(Pair(fileName, fileBytes))
                    }

                    else -> {}
                }
            }

            return formData
        }
    }
}