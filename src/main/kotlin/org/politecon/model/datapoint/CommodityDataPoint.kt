package org.politecon.model.datapoint

import kotlinx.serialization.Serializable
import org.politecon.model.Country

/**
 * Represents a data point for a commodity
 */
@Serializable
class CommodityDataPoint {
    lateinit var timeFrame: String
    lateinit var country: Country
    lateinit var source: String
    lateinit var commodityDimension: CommodityDimension
    lateinit var value: DataPointValue<Double>

    fun valid() = true // FIXME
    fun toMatrix(): Array<Array<String>> {
        TODO()
    }

    override fun toString(): String {
        return "CommodityDataPoint(timeFrame='$timeFrame', country=$country, source='$source', commodityDimension=$commodityDimension, value=$value)"
    }


    companion object {
        fun create(init: CommodityDataPoint.() -> Unit): CommodityDataPoint {
            val commodityDataPoint = CommodityDataPoint()
            init.invoke(commodityDataPoint)

            if (!commodityDataPoint.valid()) {
                println("incomplete datapoint initialization")
            }

            return commodityDataPoint
        }
    }
}