package models

@CouchBaseCollection("countries")
class Country : BaseModel() {
    var shortName: String = ""

    var name: String = ""
}