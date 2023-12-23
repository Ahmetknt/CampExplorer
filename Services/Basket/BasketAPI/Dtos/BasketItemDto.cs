using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampExplorer.Services.Basket.Dtos
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }

        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }

        public decimal Price { get; set; }
    }
}