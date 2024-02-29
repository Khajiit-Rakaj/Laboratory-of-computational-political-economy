namespace LCPE.Data.DataUploadViewModels;

public class CsvDataUploadViewModel
{
    public string Data { get; set; }

    public string Model { get; set; }

    public List<KeyValuePair<string, string>> SourceDestinationPath { get; set; }
}