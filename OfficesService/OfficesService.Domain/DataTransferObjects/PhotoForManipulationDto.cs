using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace OfficesService.Domain.DataTransferObjects
{
    public class PhotoForManipulationDto
    {
        public IFormFile Url { get; set; }
    }
}
