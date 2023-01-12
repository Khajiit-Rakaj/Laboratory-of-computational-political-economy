package org.politecon.model

enum class DataSubject(val allowedUnits: Set<Units>) {
    NATURAL_GAS(setOf(Units.JOULE, Units.M3)),
    ELECTRICITY(setOf(Units.WATT_HOUR)),
    POPULATION_POVERTY(setOf(Units.NUMBER, Units.PERCENT)),
    UNKNOWN(setOf())
}
