namespace iFindMongo.Models;

public class iFindDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string SearchTermCollectionName { get; set; } = null!;
}
