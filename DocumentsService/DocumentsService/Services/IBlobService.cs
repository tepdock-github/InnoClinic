﻿using DocumentService.Domain.DataTransferObjects;
using PdfSharpCore.Pdf;

namespace DocumentsService.Services
{
    public interface IBlobService
    {
        Task<IEnumerable<BlobDto>> GetAllBlobsAsync();
        Task<BlobManipulationDto> UploadFileAsync(IFormFile file);
        Task<BlobDto?> DownloadDtoAsync(string filename);
        Task DeleteFileDto(string filename);
    }
}
