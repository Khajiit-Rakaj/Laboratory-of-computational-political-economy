package org.politecon.importer

import org.politecon.loaders.IStreamLoader

class DataFromFileImporter : IDataFromFileImporter {
    val loaders: Array<IStreamLoader> = arrayOf<IStreamLoader>()

    constructor() {

    }

    override fun importDataFromFile(
        fileByteArray: ByteArray,
        dataModel: String,
        collection: String,
        scope: String
    ): Boolean {
        return true;
    }
}