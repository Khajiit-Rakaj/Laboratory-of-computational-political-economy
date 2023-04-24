package repositories

import com.couchbase.client.kotlin.Cluster
import com.couchbase.client.kotlin.Scope
import models.BaseModel
import models.CouchBaseCollection
import org.politecon.common.datamodel.datapoint.logger
import java.io.IOException
import java.util.*
import kotlin.collections.ArrayList
import kotlin.reflect.KClass
import kotlin.reflect.typeOf
import kotlin.time.Duration.Companion.seconds

open class BaseRepository<T : BaseModel> {
    protected var couchBaseCollectionName: String = ""

    // TODO: move this code to json config file and resolve them from there
    private val url = "couchbase://127.0.0.1"
    private val username = "politecon"
    private val password = "politecon"

    private val bucketName = "politecon"
    private val scopeName = "_default"
    protected var cluster: Cluster? = null

    constructor(kClass: KClass<T>) {
        couchBaseCollectionName = getCollectionName(kClass) ?: ""
    }

    protected suspend inline fun <reified T : BaseModel> saveItems(items: ArrayList<T>) {
        val connection = connectScope()
        items.forEach {
            connection?.collection(couchBaseCollectionName)?.upsert(it.id ?: UUID.randomUUID().toString(), it)
        }
        cluster?.disconnect()
    }

    protected suspend inline fun <reified T : BaseModel> getItems(ids: ArrayList<String>): ArrayList<T> {
        val result: ArrayList<T> = arrayListOf()
        val connection = connectScope()
        ids.forEach {
            val item = connection?.collection(couchBaseCollectionName)?.get(it)?.contentAs<T>()
            result.add(item!!)
        }
        cluster?.disconnect()

        return result
    }

    protected inline fun <reified T : BaseModel> getCollectionName(): String? {
        val collectionNameAnnotation =
            typeOf<T>().annotations.find { it is CouchBaseCollection } as? CouchBaseCollection
        return collectionNameAnnotation?.name
    }

    private fun getCollectionName(kClass: KClass<T>): String? {
        var collectionNameAnnotation = kClass.annotations.find { it is CouchBaseCollection } as? CouchBaseCollection
        return collectionNameAnnotation?.name
    }

    protected suspend fun connectScope(): Scope? {
        try {
            cluster = Cluster.connect(url, username, password)
            cluster?.waitUntilReady(5.seconds)
            val bucket = cluster!!.bucket(bucketName)
            return bucket.scope(scopeName)
        } catch (ex: IOException) {
            logger.error("failed to create connection")
        }

        return null
    }
}