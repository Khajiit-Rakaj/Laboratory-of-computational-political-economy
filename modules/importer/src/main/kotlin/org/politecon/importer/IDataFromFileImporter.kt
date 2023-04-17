package org.politecon.importer

interface IDataFromFileImporter {
    fun importDataFromFile(fileByteArray: ByteArray, dataModel: String, collection: String, scope: String): Boolean
}