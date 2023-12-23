using Core.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoAPI.Services;

namespace PhotoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        IPhotoService _photoService;
        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile photo, CancellationToken cancellationToken)
        {

            var response = await _photoService.CreatePhoto(photo, cancellationToken);
            return CreateActionResultInstance(response);

        }
        [HttpDelete]
        public IActionResult Delete(string photoUrl)
        {
            var response = _photoService.DeletePhoto(photoUrl);
            return CreateActionResultInstance(response);

        }
        [HttpPut]
        public async Task<IActionResult> Update(IFormFile photo,CancellationToken cancellationToken)
        {
            var response = await _photoService.UpdatePhoto(photo,cancellationToken);
            return CreateActionResultInstance(response);

        }

    }
}
