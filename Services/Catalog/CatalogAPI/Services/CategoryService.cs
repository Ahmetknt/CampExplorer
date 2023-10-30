using AutoMapper;
using CatalogAPI.Dtos;
using CatalogAPI.Model;
using CatalogAPI.Settings;
using MongoDB.Driver;
using Core.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories),200);
        }

        public async Task<Response<NoContent>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);

            if(category != null) {
                await _categoryCollection.InsertOneAsync(category);
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Category not added",404);
        }

        public async Task<Response<NoContent>> UpdateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var updatedCategory = await _categoryCollection.FindOneAndReplaceAsync(x => x.Id == category.Id, category);
            if (updatedCategory != null) {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Category not found", 404);
        }

        public async Task<Response<NoContent>> DeleteAsync(string categoryId)
        {
            var deletedCategory = await _categoryCollection.DeleteOneAsync(x => x.Id == categoryId);
            if (deletedCategory.DeletedCount != 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Category not found", 404);
        }
        public async Task<Response<CategoryDto>> GetAllCategoryById(string id)
        {
            var category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404);
            }
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}

