using LCPE.Data.Interfaces.Repositories;
using LCPE.Data.Queries;
using LCPE.Interfaces.DataModels;

namespace LCPE.Data.DataUploaders
{
    public class ProductDataSaver : BaseEntityDataSaver<ProductData, ProductQuery>
    {
        public ProductDataSaver(IProductRepository productRepository) : base(productRepository) { }
    }
}
