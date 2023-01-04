package org.politecon.util

import java.util.regex.Matcher
import java.util.regex.Pattern

/**
 * Utility class to handle dynamic string templates
 *
 * usage: makeString("\${a} # \${b} @ \${c}", mapOf("a" to 123, "c" to "xyz"))   // => "123 # ??? @ xyz"
 */
object Substitutions {
    private val pattern = Pattern.compile("#\\{([^}]+)}")

    fun makeString(
        template: String,
        dataMap: Map<String, Any?>,
        undefinedStub: String = "???"
    ): String {
        val replacer = createReplacer(dataMap, undefinedStub)
        val messageParts = splitWithDelimiters(template, pattern, replacer)

        return messageParts.joinToString("")
    }

    private fun createReplacer(dataMap: Map<String, Any?>, stub: String): (Matcher) -> String {
        return { m ->
            val key = m.group(1)
            (dataMap[key] ?: stub).toString()
        }
    }

    private fun splitWithDelimiters(
        text: String,
        pattern: Pattern,
        matchTransform: (Matcher) -> String
    ): List<String> {
        var lastMatch = 0
        val items = mutableListOf<String>()
        val m = pattern.matcher(text)

        while (m.find()) {
            items.add(text.substring(lastMatch, m.start()))
            items.add(matchTransform(m))
            lastMatch = m.end()
        }

        items.add(text.substring(lastMatch))

        return items
    }
}