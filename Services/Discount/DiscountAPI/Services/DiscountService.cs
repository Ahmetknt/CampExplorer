using Core.Dtos;
using Dapper;
using DiscountAPI.Model;
using Npgsql;
using System.Data;

namespace DiscountAPI.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDbConnection _dbConnection;
        private readonly IConfiguration _configuration;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));

        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var response = await _dbConnection.ExecuteAsync("delete from discount where id = @Id", new { Id = id });
            return response > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);
               
        }

        public async Task<Response<List<Discount>>> GetAll()
        {
            var response = await _dbConnection.QueryAsync<Discount>("Select * from discount");
          
            return response != null ? Response<List<Discount>>.Success(response.ToList(), 204) : Response<List<Discount>>.Fail("Discount not found", 404);
        }

        public async Task<Response<Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var response = await _dbConnection.QueryAsync<Discount>("Select * from discount where userId = @UserId and code = @Code",new {UserId = userId,Code = code});
            var hasResponse = response.FirstOrDefault();
            if(hasResponse != null)
            {
                return Response<Discount>.Success(hasResponse, 200);
            }
            return Response<Discount>.Fail("Discount not found", 404);

        }

        public async Task<Response<Discount>> GetById(int id)
        {
            var response = (await _dbConnection.QueryAsync("select * from discount where id=@Id", new { Id = id })).SingleOrDefault();
            var hasResponse = response.FirstOrDefault();
            if(hasResponse != null)
            {
                return Response<Discount>.Success(hasResponse, 200);
            }
            return Response<Discount>.Fail("Discount not found", 404);
        }

        public async Task<Response<NoContent>> Save(Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);

            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("an error occurred while adding", 500);
        }

        public async Task<Response<NoContent>> Update(Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id", new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate });

            if (status > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("Discount not found", 404);
        }
    }
}
