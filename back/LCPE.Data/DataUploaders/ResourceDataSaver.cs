using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataUploaders
{
    public class ResourceDataSaver : BaseEntityDataSaver<ResourceData, ResourceQuery>
    {
        public ResourceDataSaver(IResourceRepository resourceRepository) : base(resourceRepository)
        {
        }
    }
}
