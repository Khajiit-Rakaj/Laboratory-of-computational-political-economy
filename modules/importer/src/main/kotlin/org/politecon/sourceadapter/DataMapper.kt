package org.politecon.sourceadapter

import com.neovisionaries.i18n.CountryCode
import org.politecon.common.datamodel.*

interface DataMapper {
    fun countryFromExternal(external: String): CountryCode
    fun areaFromExternal(external: String) : Area
    fun subjectFromExternal(external: String): DataSubject
    fun dimensionFromExternal(external: String): DataDimension
    fun unitFromExternal(external: String): DataUnit
    fun sexFromExternal(external: String) : Sex

    fun countryToExternal(countryCode: CountryCode): String
    fun areaToExternal(area: Area):String
    fun subjectToExternal(subject: DataSubject): String
    fun dimensionToExternal(dataDimension: DataDimension): String
    fun unitToExternal(dataUnit: DataUnit): String
    fun sexToExternal(sex: Sex):String
}