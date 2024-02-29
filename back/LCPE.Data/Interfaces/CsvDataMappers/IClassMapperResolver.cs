namespace LCPE.Data.Interfaces.CsvDataMappers;

public interface IClassMapperResolver
{
    Type ResolveMapper(Type entityType);
}