using ESourcing.Product.Data.Interfaces;
using ESourcing.Product.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESourcing.Product.Repositories
{
    public class ProductRepository :IProductRepository
    {
        private readonly IProductContext _context;

        public ProductRepository(IProductContext productContext)
        {
            _context = productContext ?? throw new ArgumentNullException(nameof(productContext));
        }

        public async Task<IEnumerable<Entites.Product>> GetProducts()
        {
            return await _context
                            .Products
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<Entites.Product> GetProduct(string id)
        {
            return await _context
                            .Products
                            .Find(p => p.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Entites.Product>> GetProductByName(string name)
        {
            FilterDefinition<Entites.Product> filter = Builders<Entites.Product>.Filter.ElemMatch(p => p.Name, name);

            return await _context
                          .Products
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task<IEnumerable<Entites.Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Entites.Product> filter = Builders<Entites.Product>.Filter.Eq(p => p.Category, categoryName);

            return await _context
                          .Products
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task Create(Entites.Product product)
        {
            await _context.Products.InsertOneAsync(product);

        }

        public async Task<bool> Update(Entites.Product product)
        {
            var updateResult = await _context
                                        .Products
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Entites.Product> filter = Builders<Entites.Product>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Products
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
