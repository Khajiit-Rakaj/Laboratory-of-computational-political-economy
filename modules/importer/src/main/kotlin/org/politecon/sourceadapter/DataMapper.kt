package org.politecon.sourceadapter

import com.neovisionaries.i18n.CountryCode
import org.politecon.model.DataDimension
import org.politecon.model.DataSubject
import org.politecon.model.DataUnit

interface DataMapper {
    fun countryFromCode(input: String): CountryCode
    fun subjectFromCode(input: String): DataSubject
    fun dimensionFromCode(input: String): DataDimension
    fun unitFromCode(input: String): DataUnit
}