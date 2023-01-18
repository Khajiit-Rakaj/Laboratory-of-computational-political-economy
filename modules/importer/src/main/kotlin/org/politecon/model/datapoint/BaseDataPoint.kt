package org.politecon.model.datapoint

import com.neovisionaries.i18n.CountryCode
import kotlinx.serialization.Serializable
import mu.KotlinLogging

private val logger = KotlinLogging.logger {}

@Serializable
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
        internal inline  fun <reified T:BaseDataPoint>create(init: T.() -> Unit): T {
            val dataPoint = T::class.constructors.first { it.parameters.isEmpty() }.call()
            init.invoke(dataPoint)

            if (!dataPoint.valid()) {
                logger.info { "Создана невалидная точка данных $dataPoint" }
            }

            return dataPoint
        }
    }
}

