package org.politecon.model

enum class Commodity(allowedUnits: List<Units>) {
    NATURAL_GAS(listOf(Units.JOULES, Units.M3))
}
