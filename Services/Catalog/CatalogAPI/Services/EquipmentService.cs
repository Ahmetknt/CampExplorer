using AutoMapper;
using CatalogAPI.Dtos;
using CatalogAPI.Model;
using CatalogAPI.Settings;
using Core.Dtos;
using MongoDB.Driver;

namespace CatalogAPI.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IMongoCollection<Equipment> _equipmentCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public EquipmentService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _equipmentCollection = database.GetCollection<Equipment>(databaseSettings.EquipmentCollectionName); ;
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<NoContent>> CreateAsync(EquipmentCreateDto equipmentCreateDto)
        {
            var equipment = _mapper.Map<Equipment>(equipmentCreateDto);

            var categoryIdControl = await _categoryCollection.CountDocumentsAsync(x=>x.Id.Equals(equipment.CategoryId));

            if (categoryIdControl == 0) {
                return Response<NoContent>.Fail("Eklemek istediğiniz kategori id, kategori tablosunda bulunmuyor.",404);
            }
            if(equipment != null)
            {
                equipment.CreatedTime = DateTime.Now;
                await _equipmentCollection.InsertOneAsync(equipment);
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Equipment not added",404);
        }

        public async Task<Response<NoContent>> DeleteAsync(string equipmentId)
        {
            var deletedEquipment = await _equipmentCollection.DeleteOneAsync(x=>x.Id == equipmentId);
            if(deletedEquipment.DeletedCount != 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Equipment not found", 404);
        }

        public async Task<Response<List<EquipmentDto>>> GetAllAsync()
        {
            var equipments = await _equipmentCollection.Find(equipment => true).ToListAsync();

            if (equipments.Any())
            {
                equipments.ForEach(equip =>
               {
                   equip.Category = _categoryCollection.Find(x => x.Id == equip.CategoryId).First();
               });
            }
            else
            {
                equipments = new List<Equipment>();
            }

            return Response<List<EquipmentDto>>.Success(_mapper.Map<List<EquipmentDto>>(equipments), 200);
        }

        public async Task<Response<EquipmentDto>> GetAllEquipmentById(string equipmentId)
        {
            var equipment = await _equipmentCollection.Find(x => x.Id == equipmentId).FirstOrDefaultAsync();
            if (equipment == null)
            {
                return Response<EquipmentDto>.Fail("Equipment not found", 404);
            }

            equipment.Category = await _categoryCollection.Find(x=>x.Id==equipment.CategoryId).FirstAsync();
           
            return Response<EquipmentDto>.Success(_mapper.Map<EquipmentDto>(equipment), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(EquipmentDto equipmentDto)
        {
            var equipment = _mapper.Map<Equipment>(equipmentDto);
            var updatedEquipment = await _equipmentCollection.FindOneAndReplaceAsync(x => x.Id == equipment.Id, equipment);
            if (updatedEquipment != null)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Equipment not found", 404);
        }
    }
}
