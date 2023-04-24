package models.patents

import models.CouchBaseCollection

@CouchBaseCollection("patent-grants")
class PatentGrant: BasePatentModel() {
    val grantsByOfficeResident: Int? = null

    val grantsByOfficeNonResident: Int? = null

    val grantsByOriginTotal: Int? = null

    val inForceByOfficeTotal: Int? = null
}