package models

open class BaseModelWithMeta: BaseModel() {
    val metaData: Metadata = Metadata()
}