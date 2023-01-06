package org.politecon.model

enum class DataSubject(val allowedUnits: Set<Units>) {
    NATURAL_GAS(setOf(Units.JOULES, Units.M3)), POPULATION_POVERTY(setOf(Units.NUMBER, Units.PERCENT)), UNKNOWN(setOf())
}
