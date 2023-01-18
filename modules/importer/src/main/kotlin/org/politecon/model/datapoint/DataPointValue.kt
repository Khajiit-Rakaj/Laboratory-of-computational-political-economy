package org.politecon.model.datapoint

import kotlinx.serialization.Serializable
import org.politecon.model.DataUnit

@Serializable
data class DataPointValue<T>(val value: T, val dataUnit: DataUnit)