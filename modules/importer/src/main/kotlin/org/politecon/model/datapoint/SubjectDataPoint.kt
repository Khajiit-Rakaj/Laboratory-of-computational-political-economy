package org.politecon.model.datapoint

import kotlinx.serialization.Serializable
import org.politecon.model.DataDimension
import org.politecon.model.DataSubject

/**
 * Represents a data point for a commodity
 */
@Serializable
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