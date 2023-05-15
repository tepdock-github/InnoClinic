using Authorization.Domain.Interfaces;
using Authorization.Domain.Models;
using AutoMapper;

namespace AuthorizationService.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AccountService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Account> GetDto(string id)
        {
            return await _repositoryManager.AccountRepository.GetAsync(id, trackChanges: false);

        }
    }
}
