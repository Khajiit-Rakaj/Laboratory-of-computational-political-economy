using LCPE.Attributes;
using LCPE.Constants;
using LCPE.Interfaces.Enums;
using LCPE.Interfaces.DataModels.DataTypes;

namespace LCPE.Interfaces.DataModels;

[CouchBaseRelation(DataConstants.Organisation)]
public class OrganisationData : DataEntity
{
    [PresentableField(DataType.StringValue)]
    public string Name { get; set; }

    [PresentableField(DataType.StringValue)]
    public string ShortName { get; set; }

    [PresentableField(DataType.HistoricalDataArrayValue)]
    public HistoricalData[] Type {  get; set; }

    [PresentableField(DataType.HistoricalDataArrayValue)]
    public HistoricalData[] NameHistory {  get; set; }

    [PresentableField(DataType.StringValue)]
    public string Abbreviation { get; set; }

    [PresentableField(DataType.StringValue)]
    public string Country { get; set; }

    [PresentableField(DataType.HistoricalDataArrayValue)]
    public HistoricalData[] CountryHistory { get; set; }

    [PresentableField(DataType.DateValue)]
    public DateTime CreationDate { get; set; }

    [PresentableField(DataType.DateValue)]
    public DateTime EndDate { get; set; }

    [PresentableField(DataType.StringArrayValue)]
    public string[] Maternal { get; set; }

    [PresentableField(DataType.MaternalSharingArrayValue)]
    public MaternalSharing[] MaternalSharing { get; set; }
}