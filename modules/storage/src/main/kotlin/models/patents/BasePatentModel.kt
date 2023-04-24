package models.patents

import models.BaseModelWithMeta

open class BasePatentModel: BaseModelWithMeta() {
    val year: Int = 0

    val countryId: String? = null

    val organizationId: String? = null
}