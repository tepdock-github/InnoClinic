using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficesService.Data.Repositories;
using OfficesService.Domain.DataTransferObjects;
using OfficesService.Domain.Models;
using OfficesService.ImageServices;

namespace OfficesService.Controllers
{
    [Route("api/offices")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly OfficeRepository _officeRepository;
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;

        public OfficeController(OfficeRepository officeRepository, IMapper mapper, 
            ImageService imageService)
        {
            _officeRepository = officeRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        /// <summary>
        ///     Get All Officies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetOffices()
        {
            var offices = await _officeRepository.GetOfficesAsync();

            var officesDto = _mapper.Map<IEnumerable<OfficeDto>>(offices);

            return Ok(officesDto);
        }

        /// <summary>
        ///     Get Office by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetOfficeById")]
        public async Task<IActionResult> GetOfficeById(string id)
        {
            var office = await _officeRepository.GetOfficeAsync(id);
            if (office == null)
            {
                return NotFound();
            }

            var officeDto = _mapper.Map<OfficeDto>(office);

            return Ok(officeDto);
        }

        /// <summary>
        ///     Create new Office
        /// </summary>
        /// <param name="officeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOffice([FromBody] OfficeForManipulationDto officeDto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var office = _mapper.Map<Office>(officeDto);

            //for (int i = 0; i < office.PhotosList.Count; i++)
            //{
            //    var pictureBytes = Convert.FromBase64String(officeDto.PhotosList[i].Url);
            //    var stream = new MemoryStream(pictureBytes);
            //    var result = _imageService.UploadImageAsync(new FormFile(stream, 0, stream.Length, office.Address, office.Address));
            //    if (result.Result.Success)
            //    {
            //        office.PhotosList[i].Url = result.Result.Result.Url.ToString();
            //    }
            //}

            await _officeRepository.CreateOfficeAsync(office);
            var officeToReturn = _mapper.Map<OfficeDto>(office);

            return CreatedAtRoute("GetOfficeById", new { id = officeToReturn.Id }, officeToReturn);
        }

        /// <summary>
        ///     Update Office By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="officeDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffice(string id,
            [FromBody] OfficeForManipulationDto officeDto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var office = await _officeRepository.GetOfficeAsync(id);
            if (office == null)
            {
                return NotFound();
            }

            _mapper.Map(officeDto, office);
            await _officeRepository.UpdateOfficeAsync(id, office);
            return NoContent();
        }
    }
}
