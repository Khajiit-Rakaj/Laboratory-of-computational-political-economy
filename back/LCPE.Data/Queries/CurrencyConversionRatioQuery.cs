﻿using LCPE.Data.Queries.ReturnFields;
using LCPE.Data.Queries.SearchFields;
using LCPE.Data.Queries.SortingFields;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Queries
{
    public class CurrencyConversionRatioQuery : BaseQuery<CurrencyConversionRatioData, CurrencyConversionRatioReturnFields,
        CurrencyConversionRatioSearchFields, CurrencyConversionRatioSortingFields>
    {
    }
}