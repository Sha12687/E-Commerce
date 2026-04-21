using E_Commerce.Models;
using ECommerce.Business.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repo;

        public CategoryService(IGenericRepository<Category> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _repo.GetAllAsync();
        }

        public async Task CreateCategory(Category category)
        {
            await _repo.AddAsync(category);
            await _repo.SaveAsync();
        }
    }
}
