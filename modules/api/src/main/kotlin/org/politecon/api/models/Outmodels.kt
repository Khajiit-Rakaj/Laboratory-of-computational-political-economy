package org.politecon.api.models

import models.TableInfo
import org.politecon.api.plugins.TableList

fun tableListToOutmodel(tables: Array<TableInfo?>): TableList {
    val tableArr = mutableListOf<org.politecon.api.plugins.TableInfo>()
    for (table in tables) {
        if (table != null) {
            tableArr.add(org.politecon.api.plugins.TableInfo(table.tableName, table.docsNum))
        }
    }

    return TableList(tables.size, tableArr.toTypedArray())
}