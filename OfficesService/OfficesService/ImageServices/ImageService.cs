using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace OfficesService.ImageServices
{
    public class ImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;

        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;

            var cloudinarySettings = _configuration.GetSection("CloudinarySettings");
            var apiKey = cloudinarySettings.GetSection("ApiKey").Value;
            var apiSecret = cloudinarySettings.GetSection("ApiSecret").Value;
            var cloudName = cloudinarySettings.GetSection("CloudName").Value;

            string cloudianryUrl = $"cloudinary://{apiKey}:{apiSecret}@{cloudName}";

            _cloudinary = new Cloudinary(cloudianryUrl)
            {
                Api = { Secure = true }
            };
        }

        public async Task<ServiceResult<string>> UploadImageAsync(IFormFile file)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ServiceResult<string> 
                {
                    Success = false,
                    Message = uploadResult.Error.Message
                };
            }

            return new ServiceResult<string>
            {
                Success = true,
                Message = uploadResult.Url.AbsolutePath
            };
        }
    }
}
