package org.politecon.sourceadapter.oecd

import com.neovisionaries.i18n.CountryCode
import org.politecon.model.*
import org.politecon.sourceadapter.DataMapper
import org.politecon.util.mapFindByValue

class OecdDataMapper : DataMapper {
    override fun countryFromExternal(external: String): CountryCode {
        return CountryCode.getByCode(external) ?: CountryCode.UNDEFINED
    }

    override fun areaFromExternal(external: String): Area {
        TODO("Not yet implemented")
    }

    override fun subjectFromExternal(external: String): DataSubject {
        return SUBJECTS[external] ?: DataSubject.UNKNOWN
    }

    override fun dimensionFromExternal(external: String): DataDimension {
        TODO("Not yet implemented")
    }

    override fun unitFromExternal(external: String): DataUnit = UNITS[external] ?: DataUnit.UNKNOWN

    override fun sexFromExternal(external: String): Sex {
        TODO("Not yet implemented")
    }

    override fun countryToExternal(countryCode: CountryCode): String = countryCode.alpha3

    override fun areaToExternal(area: Area): String {
        TODO("Not yet implemented")
    }

    override fun subjectToExternal(subject: DataSubject): String = mapFindByValue(SUBJECTS, subject) ?: ""

    override fun dimensionToExternal(dataDimension: DataDimension): String {
        TODO("Not yet implemented")
    }

    override fun unitToExternal(dataUnit: DataUnit): String {
        TODO("Not yet implemented")
    }

    override fun sexToExternal(sex: Sex): String {
        TODO("Not yet implemented")
    }

    companion object {
        private val SUBJECTS = mapOf(
            "MANM" to DataSubject.M1,
            "MABM" to DataSubject.M3
        )

        private val UNITS = mapOf(
            "IDX" to DataUnit.INDEX_YEAR
        )


    }
}