using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace OfficesService.Domain.DataTransferObjects
{
    public class PhotoForManipulationDto
    {
        [FromForm(Name = "Url")]
        public IFormFile Url { get; set; }
    }
}
