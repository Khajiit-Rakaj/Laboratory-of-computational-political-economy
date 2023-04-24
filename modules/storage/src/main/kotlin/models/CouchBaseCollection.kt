package models

@Target(AnnotationTarget.CLASS)
annotation class CouchBaseCollection(val name: String) {
}