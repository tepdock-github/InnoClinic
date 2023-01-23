using ServicesService.Domain.Entities;

namespace ServicesService.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category> GetCategoryByIdAsync(int id, bool trackChanges);
    }
}
