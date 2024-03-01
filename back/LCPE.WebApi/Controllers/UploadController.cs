using LCPE.Business.Interfaces.Services;
using LCPE.Constants;
using LCPE.Data.DataUploadViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route($"api/{DataConstants.Upload}")]
public class UploadController : Controller
{
    private readonly ICsvDataUploaderService csvDataUploaderService;

    public UploadController(ICsvDataUploaderService csvDataUploaderService)
    {
        this.csvDataUploaderService = csvDataUploaderService;
    }

    [HttpPost]
    [Route("CsvUpload")]
    public async Task<IActionResult> CsvUpload([FromBody] CsvDataUploadViewModel csvDataUploadViewModel)
    {
        var result = csvDataUploaderService.UploadDataAsync(csvDataUploadViewModel.Data,
            csvDataUploadViewModel.SourceDestinationPath.ToDictionary(k => k.Key, v => v.Value),
            csvDataUploadViewModel.Model, csvDataUploadViewModel.MetadataSource);

        return Ok(result);
    }
}