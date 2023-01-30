package org.politecon.sourceadapter.un

import mu.KotlinLogging
import org.apache.commons.csv.CSVFormat
import org.politecon.model.datapoint.BaseDataPoint
import org.politecon.model.datapoint.DataPointValue
import org.politecon.model.datapoint.SubjectDataPoint
import org.politecon.model.datapoint.SubjectDimension
import org.politecon.sourceadapter.DataMapper
import java.io.InputStream
import java.io.InputStreamReader

private val logger = KotlinLogging.logger {}

/**
 * Загружает CSV файлы
 */
class UnCsvLoader(private val stream: InputStream?, private val dataMapper: DataMapper) {
    fun read(): Set<SubjectDataPoint> {

        val result = mutableSetOf<SubjectDataPoint>()
        if (stream != null) {
            val rows = csvFormat.parse(InputStreamReader(stream))

            rows.forEach {
                result.add(BaseDataPoint.create {
                    country = dataMapper.countryFromExternal(it["REF_AREA"])
                    subjectDimension = SubjectDimension(
                        dataMapper.subjectFromExternal(it["COMMODITY"]),
                        dataMapper.dimensionFromExternal(it["TRANSACTION"])
                    )
                    date = it["TIME_PERIOD"]
                    value = DataPointValue(it["OBS_VALUE"].toDouble(), dataMapper.unitFromExternal(it["UNIT_MEASURE"]))
                    source = "UN"
                })
            }

        } else {
            logger.info { "Не получилось открыть файл для загрузки" }
        }

        return result.filter { it.valid() }.toSet()
    }

    companion object {
        private val csvFormat =
            CSVFormat.Builder.create().setDelimiter(";").setHeader().setSkipHeaderRecord(true).build()
    }
}