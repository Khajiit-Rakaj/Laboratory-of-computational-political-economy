package org.politecon.persist

enum class DbCollection(val collectionName: String) {
    COMMODITY("commodity_data"), POPULATION("population_data"), CORPORATE_FINANCE("corporate_finance")
}