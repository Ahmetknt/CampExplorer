using Core.Dtos;
using PhotoAPI.Dtos;

namespace PhotoAPI.Services
{
    public interface IPhotoService
    {
        Task<Response<PhotoDto>> CreatePhoto(IFormFile photo, CancellationToken cancellationToken);
        Task<Response<NoContent>> UpdatePhoto(IFormFile photo, CancellationToken cancellationToken);
        Response<NoContent> DeletePhoto(string photoUrl);

    }
}
