package org.politecon.util

import java.io.InputStream

fun getResourceAsText(path: String): String? = object {}.javaClass.getResource(path)?.readText()
fun getResourceAsStream(path: String): InputStream? = object {}.javaClass.getResourceAsStream(path)
