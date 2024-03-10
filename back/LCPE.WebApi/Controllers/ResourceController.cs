using LCPE.Business.Interfaces.Services;
using LCPE.Constants;
using LCPE.Data.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route($"api/{DataConstants.Resource}")]
    public class ResourceController : BaseQueryController<ResourceQuery>
    {
        public ResourceController(IResourceService resourceService) : base(resourceService)
        {
        }
    }
}
