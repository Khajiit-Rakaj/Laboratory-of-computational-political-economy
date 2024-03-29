package org.politecon.common.datamodel.datapoint

class PatentsDataPoint {
    lateinit var corporate_name: String
    lateinit var country: String

    lateinit var grants_by_office_change_yoy: String
    var grants_by_office_resident = 0
    var grants_by_office_total = 0
    lateinit var grants_by_origin_change_yoy: String

    var grants_by_origin_total = 0
    lateinit var in_force_by_office_change_yoy: String
    var in_force_by_office_total = 0

    fun valid(): Boolean = true

    fun naturalKey() = "$corporate_name-$grants_by_office_change_yoy-$grants_by_office_resident-" +
            "$grants_by_office_total-$grants_by_origin_change_yoy-$grants_by_origin_total-$in_force_by_office_change_yoy-$in_force_by_office_total"

    override fun toString(): String {
        return "PatentsDataPoint(corporate_name=$corporate_name, grants_by_office_change_yoy=$grants_by_office_change_yoy," +
                " grants_by_office_resident=$grants_by_office_resident, grants_by_office_total=$grants_by_office_total" +
                " grants_by_origin_change_yoy=$grants_by_origin_change_yoy, grants_by_origin_total=$grants_by_origin_total" +
                " in_force_by_office_change_yoy=$in_force_by_office_change_yoy, in_force_by_office_total=$in_force_by_office_total" +
                ")"
    }

}