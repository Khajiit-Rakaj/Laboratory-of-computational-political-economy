using LCPE.Attributes;
using LCPE.Constants;
using LCPE.Interfaces.Enums;

namespace LCPE.Interfaces.DataModels;

[CouchBaseRelation(DataConstants.CorporateFinance)]
public class CorporationFinances : DataEntityWithMetadata
{
    [PresentableField(DataType.StringValue)]
    public string OrganisationId { get; set; }
    
    [PresentableField(DataType.IntValue)]
    public int Year { get; set; }

    [PresentableField(DataType.StringValue)]
    public string CurrencyId { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong Actives { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong ConstantCapital { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong Revenue { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong Deduction { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong NetRevenue { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong NetProfit { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong IncomeTax { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong PercentExpenses { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong Deprecation { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong CopperLaw13196 { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong Salaries { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong SocialSpending { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong AnotherConstantCapital { get; set; }

    [PresentableField(DataType.IntValue)]
    public ulong ResearchSpending { get; set; }

    [PresentableField(DataType.StringValue)]
    public string Notes { get; set; }
}