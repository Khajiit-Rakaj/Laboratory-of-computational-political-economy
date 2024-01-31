using LCPE.Constants;

namespace LCPE.Data.Interfaces.Repositories;

public interface ICheckableRepository
{
    Task<DiagnosticResultsType> CheckState();

    Task<bool> RestoreIndexAsync();

    public Type GetDataModel { get; }
}