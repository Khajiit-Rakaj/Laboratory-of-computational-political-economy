using LCPE.Constants;

namespace InfrastructureBuilder.Entities;

public class DataEntityCheckDetails
{
    public string DataEntityName { get; set; }

    public bool HasRepository { get; set; }

    public bool HasDataBaseIndex { get; set; }

    public DiagnosticResultsType DiagnosticResult { get; set; }

    public bool? DataBaseFixSucceed { get; set; }

    public static DataEntityCheckDetails Create(string name, bool hasRepository, bool hasDataBaseIndex, DiagnosticResultsType diagnosticResult, bool? dataBaseFixSucceed)
    {
        return new DataEntityCheckDetails
        {
            DataEntityName = name,
            HasRepository = hasRepository,
            HasDataBaseIndex = hasDataBaseIndex,
            DiagnosticResult = diagnosticResult,
            DataBaseFixSucceed = dataBaseFixSucceed,
        };
    }
}