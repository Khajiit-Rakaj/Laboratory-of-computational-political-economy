package dataSaveService

import org.politecon.loaders.FileLoaderProvider

class DataSaveService : IDataSaveService {
    override suspend fun uploadFromFile(files: ArrayList<Pair<String, ByteArray>>, type: String, collection: String, scope: String): Boolean {
        val fileLoaderProvider: FileLoaderProvider = FileLoaderProvider()
        var result: Boolean = true

        files.forEach { file ->
            val loader = fileLoaderProvider.provide(file.first, type)

            result = if (loader != null) {
                result && loader.upload(file.second.inputStream(), collection)
            } else {
                false
            }
        }

        return result
    }
}