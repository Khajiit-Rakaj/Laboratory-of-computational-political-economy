package org.politecon.api.Utils

class FormData {
    val fields: ArrayList<Pair<String, String>> = arrayListOf()
    val files: ArrayList<Pair<String, ByteArray>> = arrayListOf()

    companion object {
        fun create(): FormData{
            return FormData()
        }
    }
}