using Appoitments.Data.Repositories;
using Appoitments.Domain;
using Appoitments.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appoitments.Data
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IAppoitmentRepository _appoitmentRepository;
        private IResultRepository _resultRepository;
        private IScheduleRepository _scheduleRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IAppoitmentRepository AppoitmentRepository 
        {
            get 
            {
                if (_appoitmentRepository == null)
                    _appoitmentRepository = new AppoitmentRepository(_repositoryContext);

                return _appoitmentRepository;
            }
        }

        public IResultRepository ResultRepository
        {
            get
            {
                if (_resultRepository == null)
                    _resultRepository = new ResultRepository(_repositoryContext);

                return _resultRepository;
            }
        }

        public IScheduleRepository ScheduleRepository
        {
            get
            {
                if( _scheduleRepository == null)
                    _scheduleRepository = new ScheduleRepository(_repositoryContext);

                return _scheduleRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
