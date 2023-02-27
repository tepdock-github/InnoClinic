using AutoMapper;
using OfficesService.Domain.DataTransferObjects;
using OfficesService.Domain.Interfaces;
using OfficesService.Domain.Models;
using OfficesService.Services.Interfaces;

namespace OfficesService.Services.Implementation
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;

        public OfficeService(IOfficeRepository officeRepository, IMapper mapper)
        {
            _officeRepository = officeRepository;
            _mapper = mapper;
        }

        public async Task<OfficeDto> AddOffice(OfficeForManipulationDto officeDto)
        {
            var office = _mapper.Map<Office>(officeDto);

            //for (int i = 0; i < office.PhotosList.Count; i++)
            //{
            //    var result = officeDto.PhotosList;
            //    if (result.Result.Success)
            //    {
            //        office.PhotosList[i].Url = result.Result.Result.Url.ToString();
            //    }
            //}

            await _officeRepository.CreateOfficeAsync(office);
            return _mapper.Map<OfficeDto>(office);
        }

        public async Task<OfficeDto> GetOfficeById(string id)
        {
            var office = await _officeRepository.GetOfficeAsync(id);
            if (office == null)
                throw new BadHttpRequestException("office with id: " + id + "wasn't found");

            return _mapper.Map<OfficeDto>(office);
        }

        public async Task<IEnumerable<OfficeDto>> GetOffices()
        {

            var offices = await _officeRepository.GetOfficesAsync();

            return _mapper.Map<IEnumerable<OfficeDto>>(offices);
        }

        public async Task UpdateOffice(string id, OfficeForManipulationDto officeDto)
        {
            var office = await _officeRepository.GetOfficeAsync(id);
            if (office == null)
                throw new BadHttpRequestException("office with id: " + id + "wasn't found");

            _mapper.Map(officeDto, office);
            await _officeRepository.UpdateOfficeAsync(id, office);
        }
    }
}
