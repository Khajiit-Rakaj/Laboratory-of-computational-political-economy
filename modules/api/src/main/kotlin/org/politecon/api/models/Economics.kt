package org.politecon.api.models

import org.politecon.api.plugins.TableList
import org.politecon.common.datamodel.datapoint.SubjectDataPoint
import repository.EconomicsRepo

class EconomicsModel(_economicsRepo: EconomicsRepo) {
    private val repo: EconomicsRepo

    init {
        repo = _economicsRepo
    }

    suspend fun GetTablesList(): TableList {
        val data = repo.GetTablesList()
        return tableListToOutmodel(data)
    }

    suspend fun GetEconomicsData(): Set<SubjectDataPoint> {
        return repo.GetEconomicsData()
    }
}