package org.politecon.sourceadapter

enum class FileType(val response: String) {
    Csv("csv"),
    Xlsx("xlsx");

    companion object {
        fun byNameIgnoreCaseOrNull(input: String): FileType? {
            return FileType.values().firstOrNull { it.name.equals(input, true) }
        }
    }
}