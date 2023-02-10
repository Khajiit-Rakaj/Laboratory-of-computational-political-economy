package org.politecon.common.datamodel.datapoint

import com.neovisionaries.i18n.CountryCode
import mu.KotlinLogging

val logger = KotlinLogging.logger {}

abstract class BaseDataPoint {
    lateinit var country: CountryCode
    lateinit var source: String
    lateinit var date: String
    lateinit var value: DataPointValue<Double>

    abstract fun valid(): Boolean
    open fun naturalKey(): String = "$country-$source-$date"

    override fun toString(): String {
        return "(country=$country, source='$source', date='$date', value=$value)"
    }

    companion object {
         inline fun <reified T : BaseDataPoint> create(init: T.() -> Unit): T {
            val dataPoint = T::class.constructors.first { it.parameters.isEmpty() }.call()
            init.invoke(dataPoint)

            if (!dataPoint.valid()) {
                logger.info { "Создана невалидная точка данных $dataPoint" }
            }

            return dataPoint
        }
    }
}

