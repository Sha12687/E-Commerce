using E_Commerce.Models;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategories();
    Task CreateCategory(Category category);
}