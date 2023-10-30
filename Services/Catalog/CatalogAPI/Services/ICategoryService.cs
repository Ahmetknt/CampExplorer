using CatalogAPI.Dtos;
using CatalogAPI.Model;
using Core.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogAPI.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> GetAllCategoryById(string id);
        Task<Response<NoContent>> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<Response<NoContent>> UpdateAsync(CategoryDto categoryDto);
        Task<Response<NoContent>> DeleteAsync(string categoryId);
        
    }
}
