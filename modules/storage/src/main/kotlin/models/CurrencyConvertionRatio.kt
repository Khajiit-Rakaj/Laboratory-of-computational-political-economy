package models

import java.util.Date

class CurrencyConvertionRatio: BaseModelWithMeta() {
    var currencySoldShortName: String = ""

    var currencyBoughtShortName: String = ""

    var ratio: Double = 0.0

    var date: Date = Date()
}