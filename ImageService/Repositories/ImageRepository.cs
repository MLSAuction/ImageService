namespace ImageService.Repositories
{
    public class ImageRepository
    {
        private readonly ILogger<ImageRepository> _logger;
        private readonly IConfiguration _configuration;

        public ImageRepository (ILogger<ImageRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
    }
}
