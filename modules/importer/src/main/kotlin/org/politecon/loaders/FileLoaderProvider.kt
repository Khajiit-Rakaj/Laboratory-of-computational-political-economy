package org.politecon.loaders

import org.politecon.util.FileHelper

class FileLoaderProvider {
    private val loaders: Array<IStreamLoader> = arrayOf(ExcelFileLoader(), FredFileLoader(), UnFileLoader())

    fun provide(fileName: String, modelType: String): IStreamLoader? {
        val fileExtension = FileHelper.getTypeFromFileName(fileName)

        return loaders.firstOrNull(predicate = { it.canHandle(fileExtension, modelType) })
    }
}