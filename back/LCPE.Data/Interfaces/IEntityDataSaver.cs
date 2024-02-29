namespace LCPE.Data.Interfaces;

public interface IEntityDataSaver
{
    Type GetEntityType { get; }

    Task SaveAsync(IEnumerable<object> entities);
}