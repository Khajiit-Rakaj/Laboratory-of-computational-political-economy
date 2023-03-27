package org.politecon.loaders

import com.neovisionaries.i18n.CountryCode
import org.politecon.common.datamodel.DataDimension
import org.politecon.common.datamodel.DataSubject
import org.politecon.common.datamodel.DataUnit
import org.politecon.sourceadapter.FileType
import org.politecon.sourceadapter.ModelType
import org.politecon.sourceadapter.fred.FredCsvLoader
import org.politecon.storage.db.DbCollection
import java.io.IOException
import java.io.InputStream

class FredFileLoader(
    override val fileType: FileType = FileType.Csv,
    override val modelType: ModelType = ModelType.Fred
) : BaseStreamLoader(), IStreamLoader {
    override suspend fun upload(stream: InputStream?, collection: String): Boolean {
        return try {
            val fredCsvLoader = FredCsvLoader(stream)
            val records = fredCsvLoader.read(
                CountryCode.US,
                DataSubject.M1,
                DataDimension.INDICATOR,
                DataUnit.NUMBER
            )
            store.store(if (!collection.isNullOrBlank()) DbCollection.byNameIgnoreCaseOrNull(collection)!! else DbCollection.ECONOMICS, records)

            true
        }catch (ex: IOException){
            false
        }
    }
}