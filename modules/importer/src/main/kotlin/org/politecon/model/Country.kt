package org.politecon.model

enum class Country(val twoLetterCode: String, val threeLetterCode: String) {
    RUSSIA("RU", "RUS"), USA("US", "USA"),
    UNKNOWN("XX", "XXX")
    ;

    companion object {
        fun fromString(input: String): Country =
            values().find { input.uppercase() in listOf(it.twoLetterCode, it.threeLetterCode, it.name) } ?: UNKNOWN
    }
}