package org.politecon.sourceadapter.fred

import com.neovisionaries.i18n.CountryCode
import mu.KotlinLogging
import org.apache.commons.csv.CSVFormat
import org.politecon.common.datamodel.DataDimension
import org.politecon.common.datamodel.DataSubject
import org.politecon.common.datamodel.DataUnit
import org.politecon.common.datamodel.datapoint.BaseDataPoint
import org.politecon.common.datamodel.datapoint.DataPointValue
import org.politecon.common.datamodel.datapoint.SubjectDataPoint
import org.politecon.common.datamodel.datapoint.SubjectDimension
import java.io.InputStream
import java.io.InputStreamReader

private val logger = KotlinLogging.logger {}

/**
 *
 */
class FredCsvLoader(private val stream:InputStream?) {

    fun read(country: CountryCode, dataSubject: DataSubject, dataDimension: DataDimension, dataUnit: DataUnit): Set<SubjectDataPoint> {

        val result = mutableSetOf<SubjectDataPoint>()
        if (stream != null) {
            val rows = csvFormat.parse(InputStreamReader(stream))

            rows.forEach {
                result.add(BaseDataPoint.create {
                    this.country = country
                    subjectDimension = SubjectDimension(
                        subject = dataSubject,
                        dimension = dataDimension
                    )
                    date = it[0]
                    value = DataPointValue(it[1].toDouble(), dataUnit)
                    source = "FRED"
                })
            }
        } else {
            logger.info { "Не получилось открыть файл" }
        }

        return result
    }

    companion object {
        private val csvFormat =
            CSVFormat.Builder.create().setDelimiter(",").setHeader().setSkipHeaderRecord(true).build()
    }
}