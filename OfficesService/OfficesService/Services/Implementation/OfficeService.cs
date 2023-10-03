using AutoMapper;
using CustomExceptionMiddleware.Exceptions;
using OfficesService.Domain.DataTransferObjects;
using OfficesService.Domain.Interfaces;
using OfficesService.Domain.Models;
using OfficesService.Services.Interfaces;

namespace OfficesService.Services.Implementation
{
    public class OfficeService : IOfficeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public OfficeService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<OfficeDto> AddOffice(OfficeForManipulationDto officeDto)
        {
            var office = _mapper.Map<Office>(officeDto);

            string id = Guid.NewGuid().ToString();
            office.Id = id;

            _repositoryManager.OfficeRepository.CreateOfficeAsync(office);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<OfficeDto>(office);
        }

        public async Task<IEnumerable<OfficeDto>> GetActiveOffices()
        {
            var offices = await _repositoryManager.OfficeRepository.GetOfficesActiveAsync(trackChanges: false);

            return _mapper.Map<IEnumerable<OfficeDto>>(offices);
        }

        public async Task<OfficeDto?> GetOfficeById(string id)
        {
            var office = await _repositoryManager.OfficeRepository.GetOfficeAsync(id, trackChanges: false);
            if(office == null)
            {
                throw new NotFoundException("office with id: " + id + " wasnt found");
            }

            return _mapper.Map<OfficeDto>(office);
        }

        public async Task<IEnumerable<OfficeDto>> GetOffices()
        {
            var offices = await _repositoryManager.OfficeRepository.GetOfficesAsync(trackChanges: false);

            return _mapper.Map<IEnumerable<OfficeDto>>(offices);
        }

        public async Task UpdateOffice(string id, OfficeForManipulationDto officeDto)
        {
            var office = await _repositoryManager.OfficeRepository.GetOfficeAsync(id, trackChanges: true);
            if (office == null)
            {
                throw new NotFoundException("office with id: " + id + " wasnt found");
            }

            _mapper.Map(officeDto, office);
            await _repositoryManager.SaveAsync();
        }
    }
}
