using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Domain.Entities;
using ServicesService.Filters;
using ServicesService.ServicesInterfaces;

namespace ServicesService.Controklers
{
    [Route("/api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServicesManager _servicesManager;
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper, IServicesManager servicesManager)
        {
            _mapper = mapper;
            _servicesManager = servicesManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _servicesManager.CategoryServices.GetCategories();

            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _servicesManager.CategoryServices.GetCategoryById(id);
            if (category == null)
                return NotFound();

            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryManipulationDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            var categoryToReturn = await _servicesManager.CategoryServices.CreateCategory(category);
            return CreatedAtRoute("GetCategoryById", new { id = categoryToReturn.Id }, categoryToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _servicesManager.CategoryServices.DeleteCategory(id);
            if (success == false)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> EditCategory(int id, [FromBody] CategoryManipulationDto categoryDto)
        {
            var success = await _servicesManager.CategoryServices.EditCategory(id, categoryDto);
            if (success == false)
                return NotFound();

            return NoContent();
        }
    }
}
