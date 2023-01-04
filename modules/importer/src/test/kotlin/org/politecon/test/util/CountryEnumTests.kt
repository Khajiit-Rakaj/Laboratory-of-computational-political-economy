package org.politecon.test.util

import org.politecon.model.Country
import kotlin.test.Test
import kotlin.test.assertEquals

class CountryEnumTests {

    @Test
    fun invalidInputResultsInErrorObject() {
        assertEquals(Country.UNKNOWN, Country.fromString("invalid"))
    }

    @Test
    fun countriesFoundByNames() {
        Country.values().forEach {
           assertEquals(it, Country.fromString(it.name))
            assertEquals(it, Country.fromString(it.twoLetterCode))
            assertEquals(it, Country.fromString(it.threeLetterCode))
        }
    }
}