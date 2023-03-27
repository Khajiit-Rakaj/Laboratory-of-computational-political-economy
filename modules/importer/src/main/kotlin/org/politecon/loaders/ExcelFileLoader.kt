package org.politecon.loaders

import org.politecon.sourceadapter.FileType
import org.politecon.sourceadapter.ModelType
import org.politecon.sourceadapter.excel.ExcelLoader
import org.politecon.storage.db.DbCollection
import java.io.IOException
import java.io.InputStream

class ExcelFileLoader(
    override val fileType: FileType = FileType.Xlsx,
    override val modelType: ModelType = ModelType.Excel,
) : BaseStreamLoader(), IStreamLoader {
    private val loader: ExcelLoader = ExcelLoader(objectMapper)

    override suspend fun upload(stream: InputStream?, collection: String): Boolean {
        return try {
            val patents = loader.loadFile(stream)
            store.storeDocuments(if (!collection.isNullOrBlank()) DbCollection.byNameIgnoreCaseOrNull(collection)!! else DbCollection.PATENTS, patents)

            true
        } catch (ex: IOException) {
            false
        }
    }


}