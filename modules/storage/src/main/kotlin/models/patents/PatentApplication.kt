package models.patents

import models.CouchBaseCollection

@CouchBaseCollection("patent-applications")
class PatentApplication : BasePatentModel() {
    val applicationByResident: Int? = null

    val applicationByNonResident: Int? = null

    val applicationsByOriginTotal: Int? = null

    val pctNationalPhaseEntryOffice: Int? = null

    val pctNationalPhaseEntryOrigin: Int? = null
}