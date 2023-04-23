package org.politecon.api.models

import org.politecon.api.plugins.TableList
import org.politecon.common.datamodel.datapoint.PopulationDataPoint
import org.politecon.common.datamodel.datapoint.SubjectDataPoint
import org.politecon.common.datamodel.datapoint.PatentsDataPoint
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

    suspend fun GetPopulationData(): Set<PopulationDataPoint> {
        return repo.GetPopulationData()
    }

    suspend fun GetPatentsData(): Set<HashMap<String, String>> {
        return repo.GetPatentsData()
    }

    suspend fun GetCommodityData(): Set<SubjectDataPoint> {
        return repo.GetCommodityData()
    }

    suspend fun GetCorporateFinanceData(): Set<HashMap<String, String>> {
        return repo.GetCorporateFinanceData()
    }
}