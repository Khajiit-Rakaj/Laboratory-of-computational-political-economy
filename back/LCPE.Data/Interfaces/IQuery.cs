namespace LCPE.Data.Interfaces;

public interface IQuery<T>
{
    T Provide();
}