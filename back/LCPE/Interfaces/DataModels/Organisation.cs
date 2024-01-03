using LCPE.Attributes;
using LCPE.Constants;

namespace LCPE.Interfaces.DataModels;

[CouchBaseRelation(DataConstants.Organisation)]
public class Organisation : DataEntity
{
    public string Name { get; set; }

    public string ShortName { get; set; }
}