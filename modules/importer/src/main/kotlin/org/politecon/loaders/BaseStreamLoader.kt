package org.politecon.loaders

import com.fasterxml.jackson.module.kotlin.jacksonObjectMapper
import com.google.common.hash.Hashing
import org.politecon.sourceadapter.FileType
import org.politecon.sourceadapter.ModelType
import org.politecon.storage.db.Storage

abstract class BaseStreamLoader {
    abstract val fileType: FileType
    abstract val modelType: ModelType
    val objectMapper = jacksonObjectMapper()
    val store = Storage(objectMapper, Hashing.murmur3_128())

    fun canHandle(fileType: String, modelType: String): Boolean {
        return this.fileType == FileType.byNameIgnoreCaseOrNull(fileType) && this.modelType == ModelType.byNameIgnoreCaseOrNull(
            modelType
        )
    }
}