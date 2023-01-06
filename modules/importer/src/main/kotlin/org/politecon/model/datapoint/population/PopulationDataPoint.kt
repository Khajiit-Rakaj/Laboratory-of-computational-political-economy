package org.politecon.model.datapoint.population

import org.politecon.model.Area
import org.politecon.model.DataSubject
import org.politecon.model.Sex
import org.politecon.model.datapoint.BaseDataPoint

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