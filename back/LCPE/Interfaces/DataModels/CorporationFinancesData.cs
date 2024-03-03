using LCPE.Attributes;
using LCPE.Constants;
using LCPE.Interfaces.Enums;

namespace LCPE.Interfaces.DataModels;

[CouchBaseRelation(DataConstants.CorporateFinance)]
public class CorporationFinancesData : DataEntityWithMetadata
{
    [PresentableField(DataType.StringValue)]
    public string OrganisationId { get; set; }
    
    [PresentableField(DataType.IntValue)]
    public int Year { get; set; }

    [PresentableField(DataType.StringValue)]
    public string CurrencyId { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong Actives { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong ConstantCapital { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong Revenue { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong Deduction { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong NetRevenue { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong NetProfit { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong IncomeTax { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong PercentExpenses { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong Deprecation { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong CopperLaw13196 { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong Salaries { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong SocialSpending { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong AnotherConstantCapital { get; set; }

    [PresentableField(DataType.BigIntValue)]
    public ulong ResearchSpending { get; set; }

    [PresentableField(DataType.StringValue)]
    public string Notes { get; set; }
}