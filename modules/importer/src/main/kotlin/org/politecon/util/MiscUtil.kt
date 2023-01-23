package org.politecon.util

import com.fasterxml.jackson.databind.JsonNode
import java.io.InputStream

fun getResourceAsText(path: String): String? = object {}.javaClass.getResource(path)?.readText()
fun getResourceAsStream(path: String): InputStream? = object {}.javaClass.getResourceAsStream(path)

fun <K, V> reverseAssociations(map: Map<K, V>) = map.entries.associate { (key, value) -> value to key }
fun <K, V> mapFindByValue(map: Map<K, V>, toFind: V): K? =
    map.entries.firstOrNull { (_, value) -> value == toFind }?.key


fun childByAttribute(jsonNode: JsonNode, attribute: String, value: String): String =
    jsonNode.firstOrNull { it[attribute].textValue() == value }?.get("value")?.textValue() ?: ""