namespace LCPE.Business.Interfaces.Services;

public interface ICsvDataUploaderService
{
    Task<string> UploadDataAsync(string data, IDictionary<string, string> mapping, string entityTable);
}