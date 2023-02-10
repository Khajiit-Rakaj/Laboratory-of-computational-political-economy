package org.politecon.common.datamodel.datapoint

import org.politecon.common.datamodel.DataDimension
import org.politecon.common.datamodel.DataSubject

/**
 * Represents a data point for a commodity
 */
class SubjectDataPoint : BaseDataPoint() {
    lateinit var subjectDimension: SubjectDimension

    override fun valid(): Boolean {
        return subjectDimension.subject != DataSubject.UNKNOWN && subjectDimension.dimension != DataDimension.UNKNOWN
    }
    override fun naturalKey() = "${subjectDimension.subject}-${subjectDimension.dimension}-${super.naturalKey()}"
    override fun toString(): String {
        return "SubjectDataPoint(subjectDimension=$subjectDimension) ${super.toString()}"
    }
}