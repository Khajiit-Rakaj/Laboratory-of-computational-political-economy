package org.politecon.sourceadapter.un

import com.neovisionaries.i18n.CountryCode
import org.politecon.common.datamodel.*
import org.politecon.sourceadapter.DataMapper
import org.politecon.util.mapFindByValue

class UnCountryDataMapper : DataMapper {
    override fun countryFromExternal(external: String): CountryCode = CountryCode.getByCode(external)

    override fun areaFromExternal(external: String): Area = AREAS[external] ?: Area.UNKNOWN

    override fun subjectFromExternal(external: String): DataSubject = SUBJECTS[external] ?: DataSubject.UNKNOWN

    override fun dimensionFromExternal(external: String): DataDimension {
        TODO("Not yet implemented")
    }

    override fun unitFromExternal(external: String): DataUnit {
        TODO("Not yet implemented")
    }

    override fun sexFromExternal(external: String): Sex = SEX[external] ?: Sex.UNKNOWN

    override fun countryToExternal(countryCode: CountryCode): String = countryCode.alpha3

    override fun areaToExternal(area: Area): String = mapFindByValue(AREAS, area) ?: ""

    override fun subjectToExternal(subject: DataSubject): String = mapFindByValue(SUBJECTS, subject) ?: ""

    override fun dimensionToExternal(dataDimension: DataDimension): String {
        TODO("Not yet implemented")
    }

    override fun unitToExternal(dataUnit: DataUnit): String {
        TODO("Not yet implemented")
    }

    override fun sexToExternal(sex: Sex): String = mapFindByValue(SEX, sex) ?: ""

    companion object {
        private val SUBJECTS = mapOf(
            "SI_POV_NAHC" to DataSubject.POPULATION_POVERTY
        )

        private val AREAS = mapOf(
            "T" to Area.ALL,
            "R" to Area.RURAL,
            "U" to Area.URBAN
        )

        private val SEX = mapOf(
            "T" to Sex.ALL,
            "F" to Sex.FEMALE,
            "M" to Sex.MALE
        )
    }
}