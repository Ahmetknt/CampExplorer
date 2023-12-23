using CatalogAPI.Dtos;
using Core.Dtos;

namespace CatalogAPI.Services
{
    public interface IEquipmentService
    {
        Task<Response<List<EquipmentDto>>> GetAllAsync();
        Task<Response<EquipmentDto>> GetAllEquipmentById(string equipmentId);
        Task<Response<NoContent>> CreateAsync(EquipmentCreateDto equipmentCreateDto);
        Task<Response<NoContent>> UpdateAsync(EquipmentUpdateDto equipmentUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string equipmentId);
    }
}
