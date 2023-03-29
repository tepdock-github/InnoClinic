using AutoMapper;
using CustomExceptionMiddleware.Exceptions;
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

        public async Task<CategoryDto> CreateCategory(CategoryManipulationDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            _repositoryManager.CategoryRepository.CreateCategory(category);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(id, trackChanges: false);
            if (category == null)
                throw new NotFoundException("Category with id: "+ id + " wasn't found");

            _repositoryManager.CategoryRepository.DeleteCategory(category);
            await _repositoryManager.SaveAsync();
        }

        public async Task EditCategory(int id, CategoryManipulationDto categoryDto)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(id, trackChanges: true);
            if (category == null)
                throw new NotFoundException("Category with id: " + id + " wasn't found");

            _mapper.Map(categoryDto, category);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = await _repositoryManager.CategoryRepository.GetAllCategoriesAsync(trackChanges: false);

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryById(int id) 
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(id, trackChanges: false);
            if(category == null)
               throw new NotFoundException("Category with id: " + id + " wasn't found");

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
