using CloudinaryDotNet.Actions;

namespace OfficesService.ImageServices
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ImageUploadResult Result { get; set; }
    }
}
