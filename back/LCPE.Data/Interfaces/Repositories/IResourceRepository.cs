using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Interfaces.Repositories
{
    public interface IResourceRepository : IBaseRepository<ResourceData, ResourceQuery>
    {
    }
}
