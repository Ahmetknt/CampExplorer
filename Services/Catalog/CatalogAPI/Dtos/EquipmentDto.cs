using CatalogAPI.Model;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CatalogAPI.Dtos
{
    public class EquipmentDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
