namespace LCPE.Data.Interfaces.Repositories;

public interface ICheckableRepository
{
    Task<bool> CheckState();

    public Type GetDataModel { get; }
}