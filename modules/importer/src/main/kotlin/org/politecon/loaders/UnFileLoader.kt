package org.politecon.loaders

import org.politecon.sourceadapter.FileType
import org.politecon.sourceadapter.ModelType
import org.politecon.sourceadapter.un.UnCsvLoader
import org.politecon.sourceadapter.un.UnEnergyDataMapper
import org.politecon.storage.db.DbCollection
import java.io.IOException
import java.io.InputStream

class UnFileLoader(
    override val fileType: FileType = FileType.Csv,
    override val modelType: ModelType = ModelType.Un
) :
    BaseStreamLoader(), IStreamLoader {
    override suspend fun upload(stream: InputStream?, collection: String): Boolean {
        return try {
            val unCsvLoader = UnCsvLoader(stream, UnEnergyDataMapper())
            val records = unCsvLoader.read()
            store.store(if (!collection.isNullOrBlank()) DbCollection.byNameIgnoreCaseOrNull(collection)!! else DbCollection.COMMODITY, records)

            true
        }catch (ex: IOException){
            false
        }
    }
}