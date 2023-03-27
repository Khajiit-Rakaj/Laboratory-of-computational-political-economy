package org.politecon.util

class FileHelper {
    companion object{
        fun getTypeFromFileName(fileName: String): String = fileName.substringAfterLast(".")
    }
}