using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CatalogAPI.Dtos
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
