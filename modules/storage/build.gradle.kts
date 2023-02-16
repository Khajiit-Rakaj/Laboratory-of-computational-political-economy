plugins {
    kotlin("jvm")
}

version = "unspecified"

repositories {
    mavenCentral()
}

val couchBaseVersion = "1.1.1"
dependencies {
    implementation(project(":modules:common"))

    // База данных
    implementation("com.couchbase.client:kotlin-client:$couchBaseVersion")
}