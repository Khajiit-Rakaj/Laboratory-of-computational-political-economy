﻿using LCPE.Data.Queries.ReturnFields;
using LCPE.Data.Queries.SearchFields;
using LCPE.Data.Queries.SortingFields;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Queries;

public class CorporationFinancesQuery : BaseQuery <CorporationFinances, CorporationFinancesReturnFields, CorporationFinancesSearchFields, CorporationFinancesSortingFields>
{
}