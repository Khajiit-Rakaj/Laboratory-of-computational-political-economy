using LCPE.Business.Interfaces.Services;
using LCPE.Constants;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route($"api/{DataConstants.Organisation}")]
public class OrganisationController : BaseQueryController<OrganisationData, OrganisationQuery>
{
    public OrganisationController(IOrganisationService countryService) : base(countryService)
    {
    }
}