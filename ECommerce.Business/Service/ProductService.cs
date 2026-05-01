using E_Commerce.Data2.Models;
using ECommerce.Data2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
            => await _repo.GetAllAsync();

        public async Task<Product> GetProductById(int id)
            => await _repo.GetByIdAsync(id);

        public async Task CreateProduct(Product product)
        {
            await _repo.AddAsync(product);
            await _repo.SaveAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _repo.Update(product);
            await _repo.SaveAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            _repo.Delete(product);
            await _repo.SaveAsync();
        }
    }
}
