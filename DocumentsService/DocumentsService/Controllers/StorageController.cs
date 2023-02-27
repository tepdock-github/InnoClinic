using DocumentsService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsService.Controllers
{
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IBlobService _blob;

        public StorageController(IBlobService blob)
        {
            _blob = blob;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get() =>
            Ok(await _blob.GetAllBlobsAsync());

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file) =>
            Ok(await _blob.UploadFileAsync(file));

        [HttpGet("{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            var file = await _blob.DownloadDtoAsync(filename);

            return File(file.Content, file.ContentType, file.Name);
        }

        [HttpDelete("filename")]
        public async Task<IActionResult> Delete(string filename)
        {
            await _blob.DeleteFileDto(filename);

            return NoContent();
        }
    }
}
