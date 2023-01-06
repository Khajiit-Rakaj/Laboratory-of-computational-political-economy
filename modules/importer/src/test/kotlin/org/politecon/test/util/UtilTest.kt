package org.politecon.test.util

import org.politecon.util.getResourceAsText
import kotlin.test.Test
import kotlin.test.assertNotNull

class UtilTest {

    @Test
    fun fileLoaderTest() {
        val banner = getResourceAsText("/banner.txt")

        assertNotNull(banner)
    }
}