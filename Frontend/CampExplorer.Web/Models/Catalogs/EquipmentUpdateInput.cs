using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CampExplorer.Web.Models.Catalogs
{
    public class EquipmentUpdateInput
    {
        public string Id { get; set; }

        [Display(Name = "Ekipman ismi")]
        public string Name { get; set; }

        [Display(Name = "Ekipman açıklama")]
        public string Description { get; set; }

        [Display(Name = "Ekipman fiyat")]
        public decimal Price { get; set; }

        public string Picture { get; set; }

        [Display(Name = "Ekipman kategori")]
        public string CategoryId { get; set; }

        [Display(Name = "Ekipman Resim")]
        public IFormFile PhotoFormFile { get; set; }
    }
}