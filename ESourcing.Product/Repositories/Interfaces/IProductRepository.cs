using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESourcing.Product.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Entites.Product>> GetProducts();
        Task<Entites.Product> GetProduct(string id);
        Task<IEnumerable<Entites.Product>> GetProductByName(string name);
        Task<IEnumerable<Entites.Product>> GetProductByCategory(string categoryName);

        Task Create(Entites.Product product);
        Task<bool> Update(Entites.Product product);
        Task<bool> Delete(string id);
    }
}
