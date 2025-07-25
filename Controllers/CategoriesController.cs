using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Models;
using Supermarket.API.Resource;
using Supermarket.API.Extensions;

namespace Supermarket.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, ILogger<CategoriesController> logger, ICategoryService categoryService, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryResource>> GetAllCategories()
        {
            _logger.LogInformation("Fetching all categories");
            var categories = await _categoryService.GetAllCategoriesAsync();
            var mappingCategoryResource = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
            return mappingCategoryResource;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] SaveCategoryResource saveCategory)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating category");
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            // map our new resource to our category model class using AutoMapper
            var category = _mapper.Map<SaveCategoryResource, Category>(saveCategory);
            if (category == null)
            {
                _logger.LogError("Category mapping failed");
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid category data");
            }

            // Fix: Since CreateCategoryAsync returns void, remove assignment to a variable
            // save our new category
            await _categoryService.CreateCategoryAsync(category);

            return Ok(new
            {
                isSuccess = true,
                message = "Category created successfully",
                data = _mapper.Map<Category, CategoryResource>(category)
            });
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] SaveCategoryResource saveCategoryResource)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating category");
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
                                           // the incoming data, to => Category domain model
            var updateCategory = _mapper.Map<SaveCategoryResource, Category>(saveCategoryResource);

            var result = await _categoryService.UpdateCategoryService(categoryId, updateCategory);

            if (result == null)
            {
                _logger.LogError("Category mapping failed for {categoryId}", categoryId);
                return StatusCode(StatusCodes.Status404NotFound, "Category Id not found {categoryId}");
            }

            var categoryResource = _mapper.Map<Category, CategoryResource>(result);
            return Ok(categoryResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleteCategory = await _categoryService.DeleteCategoryAsync(id);
            if (deleteCategory == null)
            {
                _logger.LogWarning($"The category with {id} does not exist");
                return StatusCode(StatusCodes.Status404NotFound, $"Category {id} was not found");
            }

            var categoryResource = _mapper.Map<Category, CategoryResource>(deleteCategory);
            
            _logger.LogInformation($"Category with ID {id} deleted successfully");
            return Ok(new
            {
                isSuccess = true,
                message = "Category deleted successfully",
                data = categoryResource
            });

        }
    }
}

    
