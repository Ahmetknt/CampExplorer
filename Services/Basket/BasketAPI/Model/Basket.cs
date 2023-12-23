namespace BasketAPI.Model
{
    public class Basket
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public decimal TotalPrice
        {
            get => BasketItems.Sum(x => x.Price * x.Quantity);
        }
    }
}
