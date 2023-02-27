using ServicesService.Domain.DataTransferObjects;

namespace ServicesService.ServicesInterfaces
{
    public interface ICategoryServices
    {
        Task DeleteCategory(int id);
        Task<CategoryDto> CreateCategory(CategoryManipulationDto categoryDto);
        Task EditCategory(int id, CategoryManipulationDto categoryDto);
        Task<CategoryDto> GetCategoryById(int id);
        Task<IEnumerable<CategoryDto>> GetCategories();
    }
}
