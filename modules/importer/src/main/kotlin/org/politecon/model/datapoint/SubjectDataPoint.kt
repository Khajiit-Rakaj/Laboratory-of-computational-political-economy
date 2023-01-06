package org.politecon.model.datapoint

import kotlinx.serialization.Serializable

/**
 * Represents a data point for a commodity
 */
@Serializable
class SubjectDataPoint : BaseDataPoint() {
    lateinit var subjectDimension: SubjectDimension

    override fun valid() = true // FIXME
    override fun naturalKey() = "${subjectDimension.subject}-${subjectDimension.dimension}-${super.naturalKey()}"

    override fun toString(): String {
        return "CommodityDataPoint(timeFrame='$date', country=$country, source='$source', commodityDimension=$subjectDimension, value=$value)"
    }
}