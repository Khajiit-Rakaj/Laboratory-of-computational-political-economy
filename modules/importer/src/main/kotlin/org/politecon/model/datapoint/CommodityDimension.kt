package org.politecon.model.datapoint

import kotlinx.serialization.Serializable
import org.politecon.model.Commodity
import org.politecon.model.DataDimension

@Serializable
data class CommodityDimension(val commodity: Commodity, val dimension: DataDimension)