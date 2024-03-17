using LCPE.Business.Interfaces.Services;
using LCPE.Constants;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route($"api/{DataConstants.MetadataSource}")]
public class MetadataSourceController : BaseQueryController<MetadataSource, MetadataSourceQuery>
{
    public MetadataSourceController(IMetadataSourceService service) : base(service)
    {
    }
}