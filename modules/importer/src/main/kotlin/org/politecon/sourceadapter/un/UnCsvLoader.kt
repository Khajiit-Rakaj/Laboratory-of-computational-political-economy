package org.politecon.sourceadapter.un

import mu.KotlinLogging
import org.apache.commons.csv.CSVFormat
import org.politecon.model.datapoint.BaseDataPoint
import org.politecon.model.datapoint.DataPointValue
import org.politecon.model.datapoint.SubjectDataPoint
import org.politecon.model.datapoint.SubjectDimension
import org.politecon.sourceadapter.DataMapper
import org.politecon.util.getResourceAsStream
import java.io.InputStreamReader

private val logger = KotlinLogging.logger {}

/**
 * Загружает CSV файлы
 */
class UnCsvLoader(private val path: String, private val dataMapper: DataMapper) {
    fun read(): Set<SubjectDataPoint> {
        val stream = getResourceAsStream(path)

        val result = mutableSetOf<SubjectDataPoint>()
        if (stream != null) {
            val rows = csvFormat.parse(InputStreamReader(stream))

            rows.forEach {
                result.add(BaseDataPoint.create {
                    country = dataMapper.countryFromCode(it["REF_AREA"])
                    subjectDimension = SubjectDimension(
                        dataMapper.subjectFromCode(it["COMMODITY"]),
                        dataMapper.dimensionFromCode(it["TRANSACTION"])
                    )
                    date = it["TIME_PERIOD"]
                    value = DataPointValue(it["OBS_VALUE"].toDouble(), dataMapper.unitFromCode(it["UNIT_MEASURE"]))
                    source = "UN"
                })
            }

        } else {
            logger.info { "Не получилось открыть файл $path" }
        }

        return result.filter { it.valid() }.toSet()
    }

    companion object {
        private val csvFormat =
            CSVFormat.Builder.create().setDelimiter(";").setHeader().setSkipHeaderRecord(true).build()
    }
}