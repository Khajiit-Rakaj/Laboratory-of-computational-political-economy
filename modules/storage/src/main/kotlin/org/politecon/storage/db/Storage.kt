package org.politecon.storage.db

import com.couchbase.client.kotlin.Bucket
import com.couchbase.client.kotlin.Cluster
import com.couchbase.client.kotlin.Scope
import com.couchbase.client.kotlin.query.execute
import com.fasterxml.jackson.core.type.TypeReference
import com.fasterxml.jackson.databind.ObjectMapper
import com.fasterxml.jackson.databind.node.ObjectNode
import com.google.common.hash.HashFunction
import models.TableInfo
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


    /**
     * Persists data to database
     */
    suspend fun store(dbCollection: DbCollection, dataPoints: Set<BaseDataPoint>) {
        val cluster = getCluster()
        val scope = getScope(getBucket(cluster))
        ensureClusterReady(cluster)

        logger.info { "Сохраняется ${dataPoints.size} точек данных в БД" }
        dataPoints.forEach {
            val collection = scope.collection(dbCollection.collectionName)
            collection.upsert(id = it.naturalKey(), content = it)
        }

        cluster.disconnect()
    }

    /**
     * Сохраняет нетипизированные документы в базу данных
     */
    suspend fun storeDocuments(dbCollection: DbCollection, dataPoints: Set<ObjectNode>) {
        val cluster = getCluster()
        val scope = getScope(getBucket(cluster))
        ensureClusterReady(cluster)

        logger.info { "Сохранятся ${dataPoints.size} документов в базу данных" }

        dataPoints.forEach {
            val collection = scope.collection(dbCollection.collectionName)
            val objectHash = hasher.hashString(objectMapper.writeValueAsString(it), Charset.defaultCharset()).toString()
            collection.upsert(id = objectHash, content = it)
        }
        cluster.disconnect()
    }

    /**
     * Загружает точки данных
     */
    suspend fun <T> get(collection: DbCollection, typeRef: TypeReference<T>, limit: Int = 10): Set<T> {
        val cluster = getCluster()
        val scope = getScope(getBucket(cluster))
        ensureClusterReady(cluster)

        val query = scope.query(
            statement = "select d.* from ${collection.collectionName} d limit $limit",
            adhoc = false
        )

        val result = query.execute()

        cluster.disconnect()

        return result.rows.map { objectMapper.readValue(it.content, typeRef) }.toSet()

    }

    suspend fun getTablesList(): Array<TableInfo?> {
        val cluster = getCluster()
        val scope = getScope(getBucket(cluster))
        ensureClusterReady(cluster)
        val result = arrayOfNulls<TableInfo>(scope.bucket.collections.getScope("_default").collections.size)
        var idx = 0
        for (collection in scope.bucket.collections.getScope("_default").collections) {
            val query = scope.query(
                statement = "select count(*) from ${collection.name}",
                adhoc = false
            )

            if (collection.name == "_default") continue

            val queryResult = query.execute()
            val count = queryResult.rows.map {
                objectMapper.readValue(
                    it.content,
                    object : TypeReference<Map<String, String>>() {})
            }[0]["$1"]
            result[idx] = TableInfo(collection.name, count?.toInt() ?: 0)
            idx++
        }

        cluster.disconnect()

        return result
    }

    private suspend fun getCluster(): Cluster {
        return Cluster.connect(
            connectionString = url,
            username = username,
            password = password,
        )
    }

    private suspend fun getBucket(cluster: Cluster): Bucket {
        return cluster.bucket(bucketName)
    }

    private suspend fun getScope(bucket: Bucket): Scope {
        return bucket.scope(scopeName)
    }

    private suspend fun ensureClusterReady(cluster: Cluster) {
        cluster.waitUntilReady(5.seconds)
    }
}