using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;

namespace ServicesService.ServicesInterfaces
{
    public interface ICategoryServices
    {
        Task DeleteCategory(int id);
        Task<CategoryDto> CreateCategory(Category category);
        Task EditCategory(int id, CategoryManipulationDto categoryDto);
        Task<Category> GetCategoryById(int id);
        Task<IEnumerable<Category>> GetCategories();
    }
}
