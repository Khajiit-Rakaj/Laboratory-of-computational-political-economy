package repositories

import models.BaseModel

interface IRepository<T> {
    suspend fun save(items: ArrayList<T>)

    suspend fun get(ids: ArrayList<String>): ArrayList<T>

//    fun search(query: IQuery<T>): ArrayList<T>
}