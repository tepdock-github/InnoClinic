using AutoMapper;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;
using ServicesService.Domain.Interfaces;
using ServicesService.ServicesInterfaces;

namespace ServicesService.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CategoryServices(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateCategory(Category category)
        {
            _repositoryManager.CategoryRepository.CreateCategory(category);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(id, trackChanges: false);
            if (category == null)
                return false;

            _repositoryManager.CategoryRepository.DeleteCategory(category);
            await _repositoryManager.SaveAsync();
            return true;
        }

        public async Task<bool> EditCategory(int id, CategoryManipulationDto categoryDto)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(id, trackChanges: true);
            if(category == null)
                return false;

            _mapper.Map(categoryDto, category);
            await _repositoryManager.SaveAsync();
            return true;
        }

        public Task<IEnumerable<Category>> GetCategories() => _repositoryManager.CategoryRepository.GetAllCategoriesAsync(trackChanges: false);

        public async Task<Category> GetCategoryById(int id) => await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(id, trackChanges: false);
    }
}
