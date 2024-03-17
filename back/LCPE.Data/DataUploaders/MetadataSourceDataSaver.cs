using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataUploaders;

public class MetadataSourceDataSaver : BaseEntityDataSaver<MetadataSource, MetadataSourceQuery>
{
    public MetadataSourceDataSaver(IMetadataSourceRepository repository) : base(repository)
    {
    }
}