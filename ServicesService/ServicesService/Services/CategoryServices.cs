using AutoMapper;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;
using ServicesService.Domain.Interfaces;
using ServicesService.ServicesInterfaces;
using System.Web.Http;

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

        public async Task DeleteCategory(int id)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(id, trackChanges: false);
            if (category == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);

            _repositoryManager.CategoryRepository.DeleteCategory(category);
            await _repositoryManager.SaveAsync();
        }

        public async Task EditCategory(int id, CategoryManipulationDto categoryDto)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(id, trackChanges: true);
            if (category == null)
                throw new BadHttpRequestException("category not found", 404);

            _mapper.Map(categoryDto, category);
            await _repositoryManager.SaveAsync();
        }

        public Task<IEnumerable<Category>> GetCategories() => _repositoryManager.CategoryRepository.GetAllCategoriesAsync(trackChanges: false);

        public async Task<Category> GetCategoryById(int id) => await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(id, trackChanges: false);
    }
}
