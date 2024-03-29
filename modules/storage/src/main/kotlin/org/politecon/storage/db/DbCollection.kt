package org.politecon.storage.db

enum class DbCollection(val collectionName: String) {
    COMMODITY("commodity_data"),
    POPULATION("population_data"),
    CORPORATE_FINANCE("corporate_finance"),
    ECONOMICS("economics"),
    PATENTS("patents");

    companion object {
        fun byNameIgnoreCaseOrNull(input: String): DbCollection? {
            return values().firstOrNull { it.name.equals(input, true) }
        }
    }
}

