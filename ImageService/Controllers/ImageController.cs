using Microsoft.AspNetCore.Mvc;
using ImageService.Repositories;
using ImageService.Models;

namespace ImageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IImageRepository _repository;

        public ImageController(ILogger<ImageController> logger, IConfiguration configuration, IImageRepository repository)
        {
            _logger = logger;
            _configuration = configuration;
            _repository = repository;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadImage()
        {
            FormFile imageFile = (FormFile)Request.Form.Files[0];

            if (imageFile == null)
            {
                return BadRequest();
            }

            ImageDTO? imageDTO = _repository.UploadImage(imageFile);

            if (imageDTO != null)
            {
                return Ok(imageDTO);
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int imageId) 
        {
            try
            {
                _repository.DeleteImage(imageId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
            return Ok();
        }
    }
}
