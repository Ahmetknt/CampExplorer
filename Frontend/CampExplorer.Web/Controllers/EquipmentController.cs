using CampExplorer.Web.Models.Catalogs;
using CampExplorer.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CampExplorer.Web.Controllers
{
    [Authorize]
    public class EquipmentController : Controller
    {
        private readonly ICatalogService _catalogService;

        public EquipmentController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }
        //http://localhost:3003/wwwroot/photos/78cc7a8c-65a3-4665-a045-b34ad5c2e3ce.png

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllEquipmentAsync());
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoryAsync();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EquipmentCreateInput equipmentCreateInput)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            
            await _catalogService.CreateEquipmentAsync(equipmentCreateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var equipment = await _catalogService.GetByEquipmentId(id);
            var categories = await _catalogService.GetAllCategoryAsync();

            if (equipment == null)
            {
                //mesaj göster
                RedirectToAction(nameof(Index));
            }
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", equipment.Id);
            EquipmentUpdateInput equipmentUpdateInput = new()
            {
                Id = equipment.Id,
                Name = equipment.Name,
                Description = equipment.Description,
                Price = equipment.Price,
                CategoryId = equipment.CategoryId,
                Picture = equipment.Picture
            };

            return View(equipmentUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EquipmentUpdateInput equipmentUpdateInput)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", equipmentUpdateInput.Id);
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            await _catalogService.UpdateEquipmentAsync(equipmentUpdateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteEquipmentAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
