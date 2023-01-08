import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
    kotlin("jvm") version "1.7.22"
    kotlin("plugin.serialization") version "1.7.22"
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

val kotlinLoggingVersion = "3.0.4"
val logbackVersion = "1.4.5"
val serializeVersion = "1.4.1"
val jacksonVersion = "2.14.1"
val ktorVersion = "2.2.1"
val couchBaseVersion = "1.1.1"
val exposedVersion = "0.40.1"
val poiVersion = "5.2.3"

dependencies {
    implementation("com.google.guava:guava:31.1-jre")

    // Логи
    implementation("io.github.microutils:kotlin-logging-jvm:$kotlinLoggingVersion")
    implementation("ch.qos.logback:logback-classic:$logbackVersion")

    // Сериализация
    implementation("org.jetbrains.kotlinx:kotlinx-serialization-core-jvm:$serializeVersion")
    implementation("com.fasterxml.jackson.module:jackson-module-kotlin:$jacksonVersion")
    implementation("com.fasterxml.jackson.dataformat:jackson-dataformat-xml:$jacksonVersion")

    // REST клиент
    implementation("io.ktor:ktor-client-core:$ktorVersion")
    implementation("io.ktor:ktor-client-cio:$ktorVersion")

    // База данных
    implementation("com.couchbase.client:kotlin-client:$couchBaseVersion")

    // Библиотека excel
    implementation("org.apache.poi:poi:$poiVersion")
    implementation("org.apache.poi:poi-ooxml:$poiVersion")

    // Библиотека стран
    implementation("com.neovisionaries:nv-i18n:1.29")

    testImplementation("org.jetbrains.kotlin:kotlin-test:1.7.22")
}

tasks.test {
    useJUnitPlatform()
}

tasks.withType<KotlinCompile>() {
    kotlinOptions.jvmTarget = "1.8"
}