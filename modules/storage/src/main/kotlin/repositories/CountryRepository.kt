package repositories

import models.CouchBaseCollection
import models.Country

class CountryRepository : BaseRepository<Country>(Country::class), ICountryRepository {
    override suspend fun save(items: ArrayList<Country>) {
        saveItems(items)
    }

    override suspend fun get(ids: ArrayList<String>): ArrayList<Country> {
        return getItems(ids)
    }

}