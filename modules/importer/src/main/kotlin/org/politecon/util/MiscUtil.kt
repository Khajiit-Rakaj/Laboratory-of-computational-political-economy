package org.politecon.util

fun getResourceAsText(path: String): String? = object {}.javaClass.getResource(path)?.readText()
