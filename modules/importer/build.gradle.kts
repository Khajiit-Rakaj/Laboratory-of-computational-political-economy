import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    kotlin("jvm")
    kotlin("plugin.serialization")
    application
}

application {
    mainClass.set("org.politecon.MainKt")
}

group = "org.politscilab"
version = "1.0"

repositories {
    mavenCentral()
}


val jacksonVersion = "2.14.1"
val ktorVersion = "2.2.1"
val couchBaseVersion = "1.1.1"
val exposedVersion = "0.40.1"
val poiVersion = "5.2.3"
val commonsCsvVersion = "1.9.0"

dependencies {

    implementation(project(":modules:common"))

    // Сериализация
    implementation("com.fasterxml.jackson.module:jackson-module-kotlin:$jacksonVersion")
    implementation("com.fasterxml.jackson.dataformat:jackson-dataformat-xml:$jacksonVersion")

    // REST клиент
    implementation("io.ktor:ktor-client-core:$ktorVersion")
    implementation("io.ktor:ktor-client-cio:$ktorVersion")

    // База данных
    implementation("com.couchbase.client:kotlin-client:$couchBaseVersion")

    // CSV
    implementation("org.apache.commons:commons-csv:$commonsCsvVersion")

    // Библиотека excel
    implementation("org.apache.poi:poi:$poiVersion")
    implementation("org.apache.poi:poi-ooxml:$poiVersion")

    // Библиотека ZIP
    implementation("net.lingala.zip4j:zip4j:2.11.3")

    testImplementation("org.jetbrains.kotlin:kotlin-test:1.7.22")
}

tasks.test {
    useJUnitPlatform()
}

