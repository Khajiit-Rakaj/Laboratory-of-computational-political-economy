namespace LCPE.Configurations;

public class CouchBaseConfiguration
{
    public string Server { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public CouchBaseOptions CouchBaseOptions { get; set; }
}