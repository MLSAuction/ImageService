using Microsoft.AspNetCore.Mvc;
using ImageService.Repositories;

namespace ImageService.Controllers
{
    public class ImageController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ImageRepository _repository;

        ImageController (ILogger logger, IConfiguration configuration, ImageRepository repository)
        {
            _logger = logger;
            _configuration = configuration;
            _repository = repository;
        }
    }
}
