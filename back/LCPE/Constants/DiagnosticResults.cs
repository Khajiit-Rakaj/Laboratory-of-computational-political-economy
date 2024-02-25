namespace LCPE.Constants;

public static class DiagnosticResults
{
    public static readonly IReadOnlyDictionary<DiagnosticResultsType, string> DiagnosticResultsMessages =
        new Dictionary<DiagnosticResultsType, string>()
        {
            { DiagnosticResultsType.Success, DiagnosticResultsType.Success.ToString() },
            { DiagnosticResultsType.Fail, DiagnosticResultsType.Fail.ToString() },
            { DiagnosticResultsType.MissingBucket, "Missing bucket" },
            { DiagnosticResultsType.MissingCollection, "Missing collection" },
            { DiagnosticResultsType.MissingScope, "Missing scope" },
            { DiagnosticResultsType.MissingRepository, "Missing repository" },
            { DiagnosticResultsType.UnableToConnect, "UnableToConnect" },
        };
}