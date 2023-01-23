using Microsoft.EntityFrameworkCore;
using ServicesService.Domain;
using ServicesService.Domain.Entities;
using ServicesService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesService.Data.Repositories
{
    public class SpecializationRepository : RepositoryBase<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateSpecialization(Specialization specialization) => Create(specialization);

        public async Task<IEnumerable<Specialization>> GetAllSpecializationsAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<Specialization> GetSpecializationAsync(int id, bool trackChanges) =>
            await FindByCondition(s => s.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();
    }
}
