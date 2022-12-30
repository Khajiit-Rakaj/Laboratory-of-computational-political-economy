package org.politecon

import com.couchbase.client.kotlin.Cluster
import com.fasterxml.jackson.databind.ObjectMapper
import com.fasterxml.jackson.dataformat.xml.XmlMapper
import com.fasterxml.jackson.module.kotlin.jacksonObjectMapper
import com.fasterxml.jackson.module.kotlin.readValue
import io.ktor.client.*
import io.ktor.client.engine.cio.*
import kotlinx.coroutines.runBlocking
import mu.KotlinLogging
import org.politecon.model.Commodity
import org.politecon.model.Country
import org.politecon.model.DataDimension
import org.politecon.model.Units
import org.politecon.model.datapoint.CommodityDataPoint
import org.politecon.model.datapoint.CommodityDimension
import org.politecon.model.datapoint.DataPointValue
import org.politecon.sourceadapter.UnRestApi
import java.time.LocalDate
import java.util.*
import kotlin.system.measureTimeMillis
import kotlin.time.Duration.Companion.seconds

private val logger = KotlinLogging.logger {}

/**
 * Starting point for application
 */
fun main() {
    val http = HttpClient(CIO)
    val xmlMapper = XmlMapper()

    val unRestApi = UnRestApi(http = http, mapper = xmlMapper)

    val elapsed = measureTimeMillis {
        runBlocking {
            val unData = unRestApi.fetchCommodity(
                countries = setOf(Country.RUSSIA, Country.USA),
                commodities = setOf(Commodity.NATURAL_GAS),
                dataDimensions = setOf(DataDimension.PRODUCTION, DataDimension.IMPORT),
                startDate = LocalDate.of(2000, 1, 1),
                endDate = LocalDate.of(2022, 1, 1)
            )
            logger.info(unData.joinToString())
        }
    }

    //doPersist(dataPoint)

    logger.info("Application finished in $elapsed ms")
}

private fun doJsonSerialization(mapper: ObjectMapper) {
    val dataPoint = CommodityDataPoint.create {
        timeFrame = "2000"
        country = Country.RUSSIA
        source = "UN"
        commodityDimension = CommodityDimension(Commodity.NATURAL_GAS, DataDimension.PRODUCTION)
        value = DataPointValue(1000.0, Units.JOULES)
    }

    val json = mapper.writeValueAsString(dataPoint)
    println(json)

    val read: CommodityDataPoint = mapper.readValue(json)

    println(read)
}

private fun doPersist(dataPoint: CommodityDataPoint) {
    val cluster = Cluster.connect(
        connectionString = "couchbase://127.0.0.1",
        username = "politsci",
        password = "politsci",
    )

    runBlocking {
        try {
            cluster.waitUntilReady(1.seconds)
            val bucket = cluster.bucket("politsci")
            val collection = bucket.defaultCollection()

            val id = UUID.randomUUID().toString()
            val result = collection.insert(id = id, content = dataPoint)
            val read = collection.get(id).contentAs<CommodityDataPoint>()

            println(read)
        } finally {
            cluster.disconnect()
        }
    }
}



