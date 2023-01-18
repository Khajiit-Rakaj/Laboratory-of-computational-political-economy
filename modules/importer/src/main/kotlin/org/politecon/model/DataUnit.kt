package org.politecon.model

import kotlinx.serialization.Serializable

@Serializable
enum class DataUnit {
    NUMBER, PERCENT, JOULE, WATT_HOUR, TON, UNKNOWN
}