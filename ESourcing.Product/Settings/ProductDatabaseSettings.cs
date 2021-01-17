namespace ESourcing.Sourcing.Settings
{
    public class ProductDatabaseSettings : IProductDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
