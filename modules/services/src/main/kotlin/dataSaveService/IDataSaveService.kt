package dataSaveService

interface IDataSaveService {
    suspend fun uploadFromFile(files: ArrayList<Pair<String, ByteArray>>, type: String, collection: String, scope: String): Boolean
}