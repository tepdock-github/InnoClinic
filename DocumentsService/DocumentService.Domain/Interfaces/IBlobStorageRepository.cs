using DocumentService.Domain.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace DocumentService.Domain.Interfaces
{
    public interface IBlobStorageRepository
    {
        Task<BlobManipulationDto> DeleteAsync(string blobFilename);
        Task<BlobDto?> DownloadAsync(string blobFilename);
        Task<List<BlobDto>> ListAsync();
        Task<BlobManipulationDto> UploadAsync(IFormFile blob);
    }
}
