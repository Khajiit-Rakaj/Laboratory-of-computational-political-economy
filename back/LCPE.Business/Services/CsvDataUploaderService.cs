using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using LCPE.Business.Interfaces.Services;
using LCPE.Data.Interfaces;
using LCPE.Data.Interfaces.CsvDataMappers;
using LCPE.Extensions;
using LCPE.Interfaces.DataModels;

namespace LCPE.Business.Services;

public class CsvDataUploaderService : ICsvDataUploaderService
{
    private readonly IEnumerable<IDynamicDataMapper> dynamicDataMappers;
    private readonly IEnumerable<IEntityDataSaver> entityDataSavers;

    public CsvDataUploaderService(IEnumerable<IDynamicDataMapper> dynamicDataMappers, IEnumerable<IEntityDataSaver> entityDataSavers)
    {
        this.dynamicDataMappers = dynamicDataMappers;
        this.entityDataSavers = entityDataSavers;
    }

    public async Task<string> UploadDataAsync(string data, IDictionary<string, string> mapping, string entityTable, string metadata)
    {
        var mapper = GetClassMapper(entityTable, mapping);
        if (mapper == null)
        {
            return "fail";
        }

        using (var reader = new StringReader(data))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csvReader.Context.RegisterClassMap(mapper as ClassMap);
            var entities = await csvReader.GetRecordsAsync(mapper.GetEntityType).ToListAsync();
            // SetMetadata(entities, metadata);
            var saver = GetClassSaver(entityTable);
            saver?.SaveAsync(entities);
        }

        return "done";
    }

    private IDynamicDataMapper? GetClassMapper(string entityTable, IDictionary<string, string> mapping)
    {
        var mapper =
            dynamicDataMappers.FirstOrDefault(x => entityTable == x.GetEntityType.GetCouchBaseRelationCollection());
        mapper?.Initialize(mapping);

        return mapper;
    }

    private IEntityDataSaver? GetClassSaver(string entityTable)
    {
        var mapper =
            entityDataSavers.FirstOrDefault(x => entityTable == x.GetEntityType.GetCouchBaseRelationCollection());

        return mapper;
    }
    //
    // private void SetMetadata(List<object> entities, string metadata)
    // {
    //     if (entities.Any() && entities.First() is DataEntityWithMetadata)
    //     {
    //         entities.ForEach(x =>
    //         {
    //             (x as DataEntityWithMetadata).Metadata =new Metadata(){  metadata;}
    //         });
    //     }
    // }
}