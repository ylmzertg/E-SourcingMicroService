using ESourcing.UI.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESourcing.UI.ApiExtension.Interfaces
{
    public interface IProductApi
    {
        Task<IEnumerable<ProductViewModel>> GetProducts();      
        Task<ProductViewModel> GetProduct(string id);
        Task<ProductViewModel> CreateProduct(ProductViewModel model);
        Task<ProductViewModel> UpdateProduct(ProductViewModel model);
        Task DeleteProductById(string id);
    }
}
