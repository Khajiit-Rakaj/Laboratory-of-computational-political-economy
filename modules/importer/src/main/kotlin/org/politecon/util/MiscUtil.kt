package org.politecon.util

import java.io.InputStream

fun getResourceAsText(path: String): String? = object {}.javaClass.getResource(path)?.readText()
fun getResourceAsStream(path: String): InputStream? = object {}.javaClass.getResourceAsStream(path)
/**
 * Переворачивает направление ассоциации
 */
fun <K, V> reverseAssociations(map: Map<K, V>) = map.entries.associate { (key, value) -> value to key }