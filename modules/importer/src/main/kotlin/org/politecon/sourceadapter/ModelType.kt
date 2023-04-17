package org.politecon.sourceadapter

enum class ModelType(val response: String) {
    Excel("excel"),
    Fred("fred"),
    Un("un");

    companion object {
        fun byNameIgnoreCaseOrNull(input: String): ModelType? {
            return values().firstOrNull { it.name.equals(input, true) }
        }
    }
}