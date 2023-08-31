namespace LCPE.Data.Interfaces;

public interface IQueryBuilder<T>
{
    T Build();
}