namespace LCPE.Data.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> GetAsync(string id);
    
    Task<IEnumerable<T>> SearchAsync();
}