using AutoMapper;
using CatalogAPI.Dtos;
using CatalogAPI.Model;

namespace CatalogAPI.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping() { 
            
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Equipment, EquipmentCreateDto>().ReverseMap();
            CreateMap<Equipment,EquipmentUpdateDto>().ReverseMap();
            CreateMap<Equipment, EquipmentDto>().ReverseMap();

        }
    }
}
