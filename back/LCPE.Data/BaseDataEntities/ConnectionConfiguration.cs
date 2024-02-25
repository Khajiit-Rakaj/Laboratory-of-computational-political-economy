namespace LCPE.Data.BaseDataEntities;

public class ConnectionConfiguration
{
    public string ConnectionEndpoint { get; private init; }

    public string User { get; private init; }

    public string Password { get; private init; }

    public string Bucket { get; private init; }

    public static ConnectionConfiguration Create(string connectionEndpoint, string user, string password, string bucket)
    {
        return new ConnectionConfiguration
        {
            ConnectionEndpoint = connectionEndpoint,
            User = user,
            Password = password,
            Bucket = bucket
        };
    }
}