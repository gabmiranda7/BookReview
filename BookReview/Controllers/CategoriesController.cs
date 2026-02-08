using BookReview.DTOs;
using BookReview.Interfaces;
using BookReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            var categoriesDtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categoriesDtos);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return BadRequest("Categoria não encontrada.");

            var category = _categoryRepository.GetCategory(categoryId);
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(categoryDto);
        }

        [HttpGet("{categoryId}/books")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public IActionResult GetBooksByCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return BadRequest("Categoria não encontrada.");
            var category = _categoryRepository.GetCategory(categoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto newCategory)
        {
            if (newCategory == null)
                return BadRequest("Dados inválidos.");

            var existingCategory = _categoryRepository.GetCategories()
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == newCategory.Name.Trim().ToUpper());
            if (existingCategory != null)
            {
                ModelState.AddModelError("", "Categoria já existente.");
                return StatusCode(422, ModelState);
            }

            var categoryToCreate = new Category
            {
                Name = newCategory.Name
            };

            if (!_categoryRepository.CreateCategory(categoryToCreate))
            {
                ModelState.AddModelError("", "Algo de errado aconteceu tentando salvar a categoria.");
                return StatusCode(500, ModelState);
            }

            return Ok("Categoria criada com sucesso.");
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null || categoryId != updatedCategory.Id)
                return BadRequest("Categoria inválida.");
            if (!_categoryRepository.CategoryExists(categoryId))
                return BadRequest("Categoria não encontrada.");
            var categoryToUpdate = _categoryRepository.GetCategory(categoryId);
            categoryToUpdate.Name = updatedCategory.Name;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _categoryRepository.UpdateCategory(categoryToUpdate);
            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return BadRequest("Categoria não encontrada");

            var categoryToDelete = _categoryRepository.GetCategory(categoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Algo de errado aconteceu tentando excluir a categoria.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}