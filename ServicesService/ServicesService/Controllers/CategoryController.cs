using Microsoft.AspNetCore.Mvc;
using ServicesService.Domain.DataTransferObjects;
using ServicesService.Filters;
using ServicesService.ServicesInterfaces;

namespace ServicesService.Controklers
{
    [Route("/api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _servicesManager;

        public CategoryController(ICategoryServices servicesManager)
        {
            _servicesManager = servicesManager;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories() =>
            Ok(await _servicesManager.GetCategories());

        /// <summary>
        /// Get category bu id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategory(int id) =>
            Ok(await _servicesManager.GetCategoryById(id));


        /// <summary>
        /// Add new category
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryManipulationDto categoryDto)
        {
            var categoryToReturn = await _servicesManager.CreateCategory(categoryDto);

            return CreatedAtRoute("GetCategoryById", new { id = categoryToReturn.Id }, categoryToReturn);
        }

        /// <summary>
        /// Delete category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _servicesManager.DeleteCategory(id);

            return NoContent();
        }


        /// <summary>
        /// Edit chosen by id category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> EditCategory(int id, [FromBody] CategoryManipulationDto categoryDto)
        {
            await _servicesManager.EditCategory(id, categoryDto);

            return NoContent();
        }
    }
}
