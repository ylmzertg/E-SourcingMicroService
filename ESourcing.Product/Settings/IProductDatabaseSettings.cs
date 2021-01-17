namespace ESourcing.Sourcing.Settings
{
    public interface IProductDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
