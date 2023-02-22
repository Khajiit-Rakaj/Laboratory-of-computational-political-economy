plugins {
    val kotlinVersion = "1.8.10"
    kotlin("jvm") version kotlinVersion
    kotlin("plugin.serialization") version kotlinVersion apply false
}

repositories {
    mavenCentral()
}

val kotlinLoggingVersion = "3.0.4"
val logbackVersion = "1.4.5"


subprojects {
    apply(plugin = "org.jetbrains.kotlin.jvm")

    dependencies {
        implementation("com.google.guava:guava:31.1-jre")

        // Библиотека стран
        implementation("com.neovisionaries:nv-i18n:1.29")

        // Логи
        implementation("io.github.microutils:kotlin-logging-jvm:$kotlinLoggingVersion")
        implementation("ch.qos.logback:logback-classic:$logbackVersion")
    }

    tasks.withType<org.jetbrains.kotlin.gradle.tasks.KotlinCompile> {
        kotlinOptions {
            jvmTarget = "11"
        }
    }

    repositories {
        mavenCentral()
    }
}