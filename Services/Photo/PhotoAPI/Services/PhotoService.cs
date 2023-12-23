using Core.Dtos;
using PhotoAPI.Dtos;
using PhotoAPI.Model;

namespace PhotoAPI.Services
{
    public class PhotoService : IPhotoService
    {
        public async Task<Response<PhotoDto>> CreatePhoto(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);
                var photoUrl = photo.FileName;
                PhotoDto photoDto = new() { PhotoUrl = photoUrl };
                return Response<PhotoDto>.Success(photoDto,200);
            }
            return Response<PhotoDto>.Fail("Photo is empty", 404);

        }

        public Response<NoContent> DeletePhoto(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);

            if (!File.Exists(path))
            {
                return Response<NoContent>.Fail("photo not found", 404);
            }
            File.Delete(path);
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> UpdatePhoto(IFormFile photo, CancellationToken cancellationToken)
        {
            DeletePhoto(photo.FileName);
            await CreatePhoto(photo, cancellationToken);
            return Response<NoContent>.Success(204);
        }
    }
}
