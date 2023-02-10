package org.politecon.common.datamodel.datapoint

import org.politecon.common.datamodel.Area
import org.politecon.common.datamodel.DataSubject
import org.politecon.common.datamodel.Sex
import org.politecon.common.datamodel.datapoint.BaseDataPoint

class PopulationDataPoint : BaseDataPoint() {

    lateinit var subject: DataSubject
    var areaConstraint = Area.ALL
    var sexConstraint = Sex.ALL
    var ageConstraint = 0..99

    override fun valid(): Boolean = true // FIXME

    override fun naturalKey() = "$subject-$areaConstraint-$sexConstraint-$ageConstraint-${super.naturalKey()}"
    override fun toString(): String {
        return "PopulationDataPoint(subject=$subject, areaConstraint=$areaConstraint, sexConstraint=$sexConstraint, ageConstraint=$ageConstraint)"
    }


}