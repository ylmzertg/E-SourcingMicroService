using MongoDB.Driver;

namespace ESourcing.Product.Data.Interfaces
{
    public interface IProductContext
    {
        IMongoCollection<Entites.Product> Products { get; }
    }
}
