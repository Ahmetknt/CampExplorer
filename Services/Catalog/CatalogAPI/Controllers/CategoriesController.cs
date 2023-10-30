using CatalogAPI.Dtos;
using CatalogAPI.Model;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.ControllerBases;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
        {
            var response = await _categoryService.CreateAsync(categoryCreateDto);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            var response = await _categoryService.UpdateAsync(categoryDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete(string categoryId)
        {
            var response = await _categoryService.DeleteAsync(categoryId);
            return CreateActionResultInstance(response);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById(string categoryId)
        {
            var response = await _categoryService.GetAllCategoryById(categoryId);
            return CreateActionResultInstance(response);
        }

    }
}
