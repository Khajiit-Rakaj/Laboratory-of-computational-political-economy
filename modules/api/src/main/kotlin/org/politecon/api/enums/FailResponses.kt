package org.politecon.api.enums

enum class FailResponses(val response: String) {
    OnlyGetMethodAvailable("Only Get method available"),
    OnlyPostMethodAvailable("Only Post method available"),
    OnlyPutMethodAvailable("Only Put method available"),
}