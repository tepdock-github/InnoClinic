namespace OfficesService.Services.Interfaces
{
    public interface IImageService
    {
        public Task<ServiceResult<string>> UploadImageAsync(IFormFile file);
    }
}
