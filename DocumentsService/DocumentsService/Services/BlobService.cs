using CustomExceptionMiddleware.Exceptions;
using DocumentService.Domain.DataTransferObjects;
using DocumentService.Domain.Interfaces;

namespace DocumentsService.Services
{
    public class BlobService : IBlobService
    {
        private readonly IBlobStorageRepository _blobStorage;

        public BlobService(IBlobStorageRepository blobStorage)
        {
            _blobStorage = blobStorage;
        }

        public async Task DeleteFileDto(string filename)
        {
            var file = await _blobStorage.DeleteAsync(filename);
            if(file == null)
                throw new NotFoundException("file with name: " + filename + "wasn't found");

            return;
        }

        public async Task<BlobDto?> DownloadDtoAsync(string filename)
        {
            var file = await _blobStorage.DownloadAsync(filename);
            if (file == null)
                throw new NotFoundException("file with name: " + filename + "wasn't found");

            return file;
        }

        public async Task<IEnumerable<BlobDto>> GetAllBlobsAsync() =>
            await _blobStorage.ListAsync();

        public async Task<BlobManipulationDto> UploadFileAsync(IFormFile file)
        {
            var blob = await _blobStorage.UploadAsync(file);
            if (blob.Error == true)
                throw new BadRequestException("smth went wrong");

            return blob;
        }
    }
}
