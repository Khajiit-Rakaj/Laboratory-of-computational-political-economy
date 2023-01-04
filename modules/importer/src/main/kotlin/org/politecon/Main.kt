package org.politecon

import com.fasterxml.jackson.dataformat.xml.XmlMapper
import io.ktor.client.*
import io.ktor.client.engine.cio.*
import kotlinx.coroutines.runBlocking
import mu.KotlinLogging
import org.politecon.model.Commodity
import org.politecon.model.Country
import org.politecon.model.DataDimension
import org.politecon.model.datapoint.CommodityDataPoint
import org.politecon.persist.Storage
import org.politecon.sourceadapter.UnRestApi
import org.politecon.util.getResourceAsText
import java.time.LocalDate
import kotlin.system.measureTimeMillis

private val logger = KotlinLogging.logger {}

/**
 * Starting point for application
 *
 * TODO add DI
 */
fun main() {

    printBanner()

    val http = HttpClient(CIO)
    val xmlMapper = XmlMapper()

    val unRestApi = UnRestApi(http = http, mapper = xmlMapper)

    val elapsed = measureTimeMillis {
        runBlocking {

            //******* Fetch Data **********
            val unData = unRestApi.fetchCommodity(
                countries = setOf(Country.RUSSIA, Country.USA),
                commodities = setOf(Commodity.NATURAL_GAS),
                dataDimensions = setOf(DataDimension.PRODUCTION, DataDimension.IMPORT),
                startDate = LocalDate.of(2000, 1, 1),
                endDate = LocalDate.of(2022, 1, 1)
            )
            logger.info("Fetched ${unData.size} data points")


            val store = Storage()
            store.store(unData)

            val dataPoints:Set<CommodityDataPoint> = store.getSlice()
            logger.info { dataPoints.joinToString() }
        }
    }

    logger.info("Application finished in $elapsed ms")
}

private fun printBanner() {
    logger.info("\n" + getResourceAsText("/banner.txt"))
}




