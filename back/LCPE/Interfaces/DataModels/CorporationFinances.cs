using LCPE.Attributes;
using LCPE.Constants;

namespace LCPE.Interfaces.DataModels;

[CouchBaseRelation(DataConstants.CorporateFinance)]
public class CorporationFinances : DataEntityWithMetadata
{
    public string OrganisationId { get; set; }
    
    public int Year { get; set; }

    public string CurrencyId { get; set; }

    public ulong Actives { get; set; }

    public ulong ConstantCapital { get; set; }

    public ulong Revenue { get; set; }

    public ulong Deduction { get; set; }

    public ulong NetRevenue { get; set; }

    public ulong NetProfit { get; set; }

    public ulong IncomeTax { get; set; }

    public ulong PercentExpenses { get; set; }

    public ulong Deprecation { get; set; }

    public ulong CopperLaw13196 { get; set; }

    public ulong Salaries { get; set; }

    public ulong SocialSpending { get; set; }

    public ulong AnotherConstantCapital { get; set; }

    public ulong ResearchSpending { get; set; }

    public string Notes { get; set; }
}