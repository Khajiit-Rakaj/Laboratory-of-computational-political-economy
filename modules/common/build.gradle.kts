plugins {
    kotlin("jvm")
    kotlin("plugin.serialization")
}

version = "unspecified"

repositories {
    mavenCentral()
}


val serializeVersion = "1.4.1"

dependencies {
    implementation(kotlin("reflect"))

    implementation("org.jetbrains.kotlinx:kotlinx-serialization-core-jvm:$serializeVersion")
}