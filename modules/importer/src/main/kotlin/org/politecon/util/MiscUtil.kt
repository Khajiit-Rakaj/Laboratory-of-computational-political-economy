package org.politecon.util

import com.fasterxml.jackson.databind.JsonNode
import mu.KotlinLogging
import net.lingala.zip4j.ZipFile
import java.io.InputStream
import java.net.URL

private val logger = KotlinLogging.logger {}

fun getResource(path: String): URL? = object {}.javaClass.getResource(path)
fun getResourceAsText(path: String): String? = getResource(path)?.readText()
fun getResourceAsStream(path: String): InputStream? = object {}.javaClass.getResourceAsStream(path)

fun getStreamFromZip(zipFilePath: String, path: String): InputStream? {
    logger.info { "Открывается стрим из $zipFilePath/$path" }
    val resource = getResource(zipFilePath)
    val zipFile = ZipFile(resource?.file)
    val fileHeader = zipFile.getFileHeader(path)
    return zipFile.getInputStream(fileHeader)
}

fun <K, V> reverseAssociations(map: Map<K, V>) = map.entries.associate { (key, value) -> value to key }
fun <K, V> mapFindByValue(map: Map<K, V>, toFind: V): K? =
    map.entries.firstOrNull { (_, value) -> value == toFind }?.key

fun childByAttribute(jsonNode: JsonNode, attribute: String, value: String): String =
    jsonNode.firstOrNull { it[attribute].textValue() == value }?.get("value")?.textValue() ?: ""