using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataUploaders
{
    public class OrganisationDataSaver : BaseEntityDataSaver<OrganisationData, OrganisationQuery>
    {
        public OrganisationDataSaver(IOrganisationRepository organisationRepository) : base(organisationRepository) { }
    }
}
