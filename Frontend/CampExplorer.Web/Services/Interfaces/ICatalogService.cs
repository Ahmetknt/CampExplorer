using CampExplorer.Web.Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampExplorer.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<EquipmentViewModel>> GetAllEquipmentAsync();

        Task<List<CategoryViewModel>> GetAllCategoryAsync();

        Task<EquipmentViewModel> GetByEquipmentId(string equipmentId);

        Task<bool> CreateEquipmentAsync(EquipmentCreateInput equipmentCreateInput);

        Task<bool> UpdateEquipmentAsync(EquipmentUpdateInput equipmentUpdateInput);

        Task<bool> DeleteEquipmentAsync(string equipmentId);
    }
}