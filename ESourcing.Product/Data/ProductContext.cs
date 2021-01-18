using ESourcing.Product.Data.Interfaces;
using ESourcing.Sourcing.Data;
using ESourcing.Sourcing.Settings;
using MongoDB.Driver;

namespace ESourcing.Product.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Entites.Product>(settings.CollectionName);

            //ProductContextSeed.SeedData(Products);
        }

        public IMongoCollection<Entites.Product> Products { get; }
    }
}
