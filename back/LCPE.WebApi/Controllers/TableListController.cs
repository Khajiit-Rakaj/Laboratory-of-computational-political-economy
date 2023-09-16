using LCPE.Business.Interfaces.Services;
using LCPE.Data.Queries;
using LCPE.Data.Queries.SearchFields;
using LCPE.Data.QueryBuilders.Couchbase;
using LCPE.Extensions;
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
        var tables = (await tableService.GetTablesAsync()).ToList();

        return Ok(tables);
    }
}