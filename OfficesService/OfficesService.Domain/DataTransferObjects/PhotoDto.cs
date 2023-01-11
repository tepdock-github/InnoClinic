using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficesService.Domain.DataTransferObjects
{
    public class PhotoDto
    {
        public required string Id { get; set; }
        public required string Url { get; set; }
    }
}
