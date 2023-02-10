package org.politecon.persist

import com.couchbase.client.kotlin.Cluster
import com.couchbase.client.kotlin.query.execute
import com.fasterxml.jackson.databind.ObjectMapper
import com.fasterxml.jackson.databind.node.ObjectNode
import com.google.common.hash.HashFunction
import mu.KotlinLogging
import org.politecon.common.datamodel.datapoint.BaseDataPoint
import java.nio.charset.Charset
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
class Storage(private val objectMapper: ObjectMapper, private val hasher: HashFunction) {
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
     * Persists data to database
     */
    suspend fun store(dbCollection: DbCollection, dataPoints: Set<BaseDataPoint>) {
        ensureClusterReady()

        logger.info { "Сохраняется ${dataPoints.size} точек данных в БД" }
        dataPoints.forEach {
            val collection = scope.collection(dbCollection.collectionName)
            collection.upsert(id = it.naturalKey(), content = it)
        }
    }

    /**
     * Сохраняет нетипизированные документы в базу данных
     */
    suspend fun storeDocuments(dbCollection: DbCollection, dataPoints: Set<ObjectNode>) {
        ensureClusterReady()

        logger.info { "Сохранятся ${dataPoints.size} документов в базу данных" }

        dataPoints.forEach {
            val collection = scope.collection(dbCollection.collectionName)
            val objectHash = hasher.hashString(objectMapper.writeValueAsString(it), Charset.defaultCharset()).toString()
            collection.upsert(id = objectHash, content = it)
        }
    }

    /**
     * Загружает точки данных
     */
    internal suspend inline fun <reified T>get(collection: DbCollection, limit: Int = 10):Set<T> {
        ensureClusterReady()

        val query = scope.query(
            statement = "select d.* from ${collection.collectionName} d limit $limit",
            adhoc = false
        )

        val result = query.execute()

        return result.rows.map { it.contentAs<T>() }.toSet()
    }

    private suspend fun ensureClusterReady() {
        cluster.waitUntilReady(5.seconds)
    }
}