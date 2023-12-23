using Core.Dtos;
using CampExplorer.Web.Helpers;
using CampExplorer.Web.Models;
using CampExplorer.Web.Models.Catalogs;
using CampExplorer.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CampExplorer.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient client, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _client = client;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateEquipmentAsync(EquipmentCreateInput equipmentCreateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(equipmentCreateInput.PhotoFormFile);

            if (resultPhotoService != null)
            {
                equipmentCreateInput.Picture = resultPhotoService.PhotoUrl;
            }

            var response = await _client.PostAsJsonAsync<EquipmentCreateInput>("equipment", equipmentCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEquipmentAsync(string equipmentId)
        {
            var response = await _client.DeleteAsync($"equipment/{equipmentId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _client.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<List<EquipmentViewModel>> GetAllEquipmentAsync()
        {
            //http:localhost:5000/services/catalog/equipment
            var response = await _client.GetAsync("equipment");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<EquipmentViewModel>>>();
            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseSuccess.Data;
        }

        public async Task<EquipmentViewModel> GetByEquipmentId(string equipmentId)
        {
            var response = await _client.GetAsync($"equipment/{equipmentId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<EquipmentViewModel>>();

            responseSuccess.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(responseSuccess.Data.Picture);

            return responseSuccess.Data;
        }

        public async Task<bool> UpdateEquipmentAsync(EquipmentUpdateInput equipmentUpdateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(equipmentUpdateInput.PhotoFormFile);

            if (resultPhotoService != null)
            {
                await _photoStockService.DeletePhoto(equipmentUpdateInput.Picture);
                equipmentUpdateInput.Picture = resultPhotoService.PhotoUrl;
            }

            var response = await _client.PutAsJsonAsync<EquipmentUpdateInput>("equipment", equipmentUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}