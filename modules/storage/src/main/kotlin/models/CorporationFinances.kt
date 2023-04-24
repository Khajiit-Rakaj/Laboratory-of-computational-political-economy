package models

@CouchBaseCollection("corporation-finances")
class CorporationFinances: BaseModelWithMeta() {
    val year: Int = 0

    val currencyId: String? = null

    val actives: Int? = null

    val constantCapital: Int? = null

    val revenue: Int? = null

    val deduction: Int? = null

    val netRevenue: Int? = null

    val netProfit: Int? = null

    val incomeTax: Int? = null

    val percentExpenses: Int? = null

    val depreciation: Int? = null

    val copperLaw13196: Int? = null

    val salaries: Int? = null

    val socialSpending: Int? = null

    val anotherConstantCapital: Int? = null

    val researchSpending: Int? = null

    val notes: String = ""
}