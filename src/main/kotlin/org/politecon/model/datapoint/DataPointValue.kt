package org.politecon.model.datapoint

import kotlinx.serialization.Serializable
import org.politecon.model.Units

@Serializable
data class DataPointValue<T>(val value: T, val units: Units)