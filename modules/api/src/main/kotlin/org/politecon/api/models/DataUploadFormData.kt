package org.politecon.api.models

class DataUploadFormData {
    var files: ArrayList<Pair<String, ByteArray>> = arrayListOf()
    var string: String? = null
    var collection: String? = null
    var type: String? = null
    var scope: String? = null

    fun isValid(): Boolean {
        return !collection.isNullOrEmpty() && !type.isNullOrEmpty() && (!files.isNullOrEmpty() || !string.isNullOrEmpty())
    }
}