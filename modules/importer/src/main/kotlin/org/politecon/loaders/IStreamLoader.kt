package org.politecon.loaders

import java.io.InputStream

interface IStreamLoader {
    fun canHandle(fileType: String, modelType: String):Boolean
    suspend fun upload(stream: InputStream?, collection: String): Boolean
}