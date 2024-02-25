using LCPE.Interfaces.DataModels;

namespace LCPE.Data.Interfaces;

public interface ICouchBaseClient<TModel> : IBaseClient<TModel>
    where TModel : DataEntity
{
}