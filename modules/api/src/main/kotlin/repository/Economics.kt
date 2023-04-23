package repository

import com.fasterxml.jackson.core.type.TypeReference
import models.TableInfo
import org.politecon.common.datamodel.datapoint.PopulationDataPoint
import org.politecon.common.datamodel.datapoint.SubjectDataPoint
import org.politecon.common.datamodel.datapoint.PatentsDataPoint
import org.politecon.storage.db.DbCollection
import org.politecon.storage.db.Storage

class EconomicsRepo(_storage: Storage) {
    private val storage: Storage

    init {
        storage = _storage
    }

    suspend fun GetTablesList(): Array<TableInfo?> {
        return storage.getTablesList()
    }

    suspend fun GetEconomicsData(): Set<SubjectDataPoint> {
        val result = storage.get(DbCollection.ECONOMICS, object : TypeReference<SubjectDataPoint>() {}, limit = 50)
        return result
    }

    suspend fun GetPopulationData(): Set<PopulationDataPoint> {
        val result = storage.get(DbCollection.POPULATION, object : TypeReference<PopulationDataPoint>() {}, limit = 50)
        return result
    }

    suspend fun GetPatentsData(): Set<PatentsDataPoint> {
        val result = storage.get(DbCollection.PATENTS, object : TypeReference<PatentsDataPoint>() {}, limit = 50)
        return result
    }

    suspend fun GetCommodityData(): Set<SubjectDataPoint> {
        val result = storage.get(DbCollection.COMMODITY, object : TypeReference<SubjectDataPoint>() {}, limit = 50)
        return result
    }

    suspend fun GetCorporateFinanceData(): Set<SubjectDataPoint> {
        val result =
            storage.get(DbCollection.CORPORATE_FINANCE, object : TypeReference<SubjectDataPoint>() {}, limit = 50)
        return result
    }
}