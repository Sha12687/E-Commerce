using E_Commerce.Data2.Models;
using E_Commerce.Models;


namespace ECommerce.Business.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task CreateCategory(Category category);

        void UpdateCategory(Category category);

        Task<Category> GetCategoryById(int id);

        Task DeleteCategory(int id);

    }

}