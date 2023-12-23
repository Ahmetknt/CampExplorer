using CatalogAPI.Dtos;
using CatalogAPI.Services;
using Core.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : CustomBaseController
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _equipmentService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EquipmentCreateDto equipmentCreateDto)
        {
            var response = await _equipmentService.CreateAsync(equipmentCreateDto);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(EquipmentUpdateDto equipmentUpdateDto)
        {
            var response = await _equipmentService.UpdateAsync(equipmentUpdateDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{equipmentId}")]
        public async Task<IActionResult> Delete(string equipmentId)
        {
            var response = await _equipmentService.DeleteAsync(equipmentId);
            return CreateActionResultInstance(response);
        }

        [HttpGet("{equipmentId}")]
        public async Task<IActionResult> GetById(string equipmentId)
        {
            var response = await _equipmentService.GetAllEquipmentById(equipmentId);
            return CreateActionResultInstance(response);
        }
    }
}
