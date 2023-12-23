namespace BasketAPI.Model
{
    public class BasketItem
    {
        public int Quantity { get; set; }
        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public decimal Price { get; set; }
    }
}
