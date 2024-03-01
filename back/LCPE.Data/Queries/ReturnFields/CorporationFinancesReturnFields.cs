namespace LCPE.Data.Queries.ReturnFields;

public class CorporationFinancesReturnFields : BaseMetadataReturnFields
{
    public bool Year { get; set; }

    public bool CurrencyId { get; set; }

    public bool Actives { get; set; }

    public bool ConstantCapital { get; set; }

    public bool Revenue { get; set; }

    public bool Deduction { get; set; }

    public bool NetRevenue { get; set; }

    public bool NetProfit { get; set; }

    public bool IncomeTax { get; set; }

    public bool PercentExpenses { get; set; }

    public bool Deprecation { get; set; }

    public bool CopperLaw13196 { get; set; }

    public bool Salaries { get; set; }

    public bool SocialSpending { get; set; }

    public bool AnotherConstantCapital { get; set; }

    public bool ResearchSpending { get; set; }
}