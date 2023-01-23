using ProfileService.Data.Repository;
using ProfilesService.Domain;
using ProfilesService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Data
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IReceptionistProfileRepository _receptionistProfile;
        private IPatientProfileRepository _patientProfile;
        private IDoctorProfileRepository _doctorProfile;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IReceptionistProfileRepository ReceptionistProfile
        {
            get
            {
                if (_receptionistProfile == null)
                    _receptionistProfile = new ReceptionistProfileRepository(_repositoryContext);
                return _receptionistProfile;
            }
        }

        public IPatientProfileRepository PatientProfile 
        {
            get
            {
                if (_patientProfile == null)
                    _patientProfile = new PatientProfileRepository(_repositoryContext);
                return _patientProfile;
            }
        }

        public IDoctorProfileRepository DoctorProfile
        {
            get
            {
                if (_doctorProfile == null)
                    _doctorProfile = new DoctorProfileRepository(_repositoryContext);
                return _doctorProfile;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
