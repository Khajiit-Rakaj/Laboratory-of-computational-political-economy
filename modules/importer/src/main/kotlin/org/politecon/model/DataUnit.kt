package org.politecon.model

import kotlinx.serialization.Serializable

@Serializable
enum class DataUnit {
    NUMBER, PERCENT, INDEX, INDEX_YEAR, TERRA_JOULE, GIGA_WATT_HOUR, TON, UNKNOWN
}