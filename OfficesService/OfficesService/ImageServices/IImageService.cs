namespace OfficesService.ImageServices
{
    public interface IImageService
    {
        public Task<ServiceResult<string>> UploadImageAsync(IFormFile file);
    }
}
