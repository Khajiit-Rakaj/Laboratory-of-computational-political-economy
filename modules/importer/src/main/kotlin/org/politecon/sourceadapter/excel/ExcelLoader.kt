package org.politecon.sourceadapter.excel

import com.fasterxml.jackson.databind.ObjectMapper
import com.fasterxml.jackson.databind.node.ObjectNode
import mu.KotlinLogging
import org.apache.poi.ss.usermodel.CellType
import org.apache.poi.ss.usermodel.Row
import org.apache.poi.ss.usermodel.WorkbookFactory
import org.politecon.util.getResourceAsStream
import java.io.InputStream

private val logger = KotlinLogging.logger {}

/**
 * Обрабатывает excel файлы
 */
class ExcelLoader(private val mapper: ObjectMapper) {

    /**
     * Переводит эксель файл в JSON
     */
    fun loadFile(stream: InputStream?): Set<ObjectNode> {
        val wb = WorkbookFactory.create(stream)

        val result = mutableSetOf<ObjectNode>()
        for (sheetIndex in 1 until wb.numberOfSheets) {
            val currentSheet = wb.getSheetAt(sheetIndex)
            logger.info { "Обрабатывается лист ${currentSheet.sheetName}" }

            // Заглавный ряд
            val headerRow = currentSheet.getRow(0)

            // Названия колонок
            val keys = headerRow.map { it.stringCellValue.lowercase().replace(" ", "_") }

            for (rowIndex in 1..currentSheet.lastRowNum) {
                logger.info { "Обрабатывается ряд $rowIndex (${currentSheet.sheetName})" }

                val dataPoint = mapper.createObjectNode()
                dataPoint.put("corporation_name", currentSheet.sheetName)

                val row = currentSheet.getRow(rowIndex)

                // Иногда захватывает null ряд
                val lastCellNum = row?.lastCellNum ?: 0

                for (cellIndex in 0 until lastCellNum) {
                    val cell = row.getCell(cellIndex, Row.MissingCellPolicy.CREATE_NULL_AS_BLANK)
                    // Если ячейка - формула, нужно развернуть тип и использовать кэшированое значение
                    when (if (cell.cellType == CellType.FORMULA) cell.cachedFormulaResultType else cell.cellType) {
                        CellType.BLANK -> dataPoint.put(keys[cellIndex], "")
                        CellType.STRING -> dataPoint.put(keys[cellIndex], cell.stringCellValue)
                        CellType.NUMERIC -> dataPoint.put(keys[cellIndex], cell.numericCellValue)
                        else -> dataPoint.put(keys[cellIndex], "UNSUPPORTED_TYPE")
                    }
                }

                result.add(dataPoint)
            }
        }

        logger.info { "Загрузка файла закончена" }
        return result
    }
}