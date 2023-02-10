package org.politecon.common.datamodel.datapoint

import org.politecon.common.datamodel.DataUnit

data class DataPointValue<T>(val value: T, val dataUnit: DataUnit)