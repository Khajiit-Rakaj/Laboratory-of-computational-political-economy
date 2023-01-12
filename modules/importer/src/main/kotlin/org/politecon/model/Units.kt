package org.politecon.model

import kotlinx.serialization.Serializable

@Serializable
enum class Units {
    NUMBER, PERCENT, JOULE, WATT_HOUR, M3
}