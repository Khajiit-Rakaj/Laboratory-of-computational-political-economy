using LCPE.Business.Interfaces.Services;
using LCPE.Constants;
using LCPE.Data.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route($"api/{DataConstants.Product}")]
    public class ProductController : BaseQueryController<ProductQuery>
    {
        public ProductController(IProductService productService) : base(productService) { }
    }
}
