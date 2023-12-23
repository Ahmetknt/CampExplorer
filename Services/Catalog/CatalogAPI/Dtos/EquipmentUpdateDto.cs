namespace CatalogAPI.Dtos
{
    public class EquipmentUpdateDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public string Picture { get; set; }

        public string CategoryId { get; set; }

    }
}
