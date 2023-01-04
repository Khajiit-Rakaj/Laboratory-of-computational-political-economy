package org.politecon.persist

import com.couchbase.client.kotlin.Cluster
import com.couchbase.client.kotlin.query.execute
import mu.KotlinLogging
import org.politecon.model.datapoint.CommodityDataPoint
import kotlin.reflect.KClass
import kotlin.time.Duration.Companion.seconds

private val logger = KotlinLogging.logger {}

/**
 * Handles saving data to DB
 *
 * TODO Cache bucket, scope and collections
 * TODO Error Handling
 * TODO Externalize properties
 * TODO Make singleton
 */
class Storage {
    private val url = "couchbase://127.0.0.1"
    private val username = "politecon"
    private val password = "politecon"

    private val bucketName = "politecon"
    private val scopeName = "_default"

    private val cluster = Cluster.connect(
        connectionString = url,
        username = username,
        password = password,
    )

    private val bucket = cluster.bucket(bucketName)
    private val scope = bucket.scope(scopeName)

    /**
     * Persists commodity data points to database
     */
    suspend fun store(dataPoints: Set<CommodityDataPoint>) {
        ensureClusterReady()

        cluster.waitUntilReady(1.seconds)
        val collection = scope.collection(collectionNames[CommodityDataPoint::class]!!)

        logger.info { "Persisting ${dataPoints.size} data points to database" }
        dataPoints.forEach {
            collection.upsert(id = it.naturalKey(), content = it)
        }
    }

    /**
     * FIXME test only
     */
    internal suspend inline fun <reified T>getSlice(limit: Int = 10):Set<T> {
        ensureClusterReady()
        val collectionName = collectionNames[T::class]!!

        val query = scope.query(
            statement = "select d.* from $collectionName d limit $limit",
            adhoc = false
        )

        val result = query.execute()

        val data = mutableSetOf<T>()
        result.rows.forEach {
            data.add(it.contentAs())
        }

        return data
    }

    private suspend fun ensureClusterReady() {
        cluster.waitUntilReady(5.seconds)
    }

    private val collectionNames:Map<KClass<*>, String> = mapOf(
        CommodityDataPoint::class to "datapoints"
    )
}