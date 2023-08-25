using LCPE.Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/TableList")]
public class TableListController : ControllerBase
{
    private readonly ITableService tableService;

    public TableListController(ITableService tableService)
    {
        this.tableService = tableService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tables = await tableService.GetTablesAsync();
        
        return Ok(/*"{\"totalNum\":7,\"tables\":[{\"tableName\":\"countries\",\"docsNum\":1},{\"tableName\":\"patents\",\"docsNum\":275},{\"tableName\":\"economics\",\"docsNum\":1009},{\"tableName\":\"corporate_finance\",\"docsNum\":773},{\"tableName\":\"population_data\",\"docsNum\":258},{\"tableName\":\"commodity_data\",\"docsNum\":18883}]}"*/tables);
    }

}