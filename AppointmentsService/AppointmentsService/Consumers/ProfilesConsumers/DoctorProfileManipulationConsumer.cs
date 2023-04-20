using Appoitments.Domain.Interfaces;
using MassTransit;
using SharedModelsInnoClinic;

namespace AppointmentsService.Consumers.ProfilesConsumers
{
    public class DoctorProfileManipulationConsumer : IConsumer<IDoctorProfileManipulation>
    {
        private readonly IRepositoryManager _repositoryManager;

        public DoctorProfileManipulationConsumer(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task Consume(ConsumeContext<IDoctorProfileManipulation> context)
        {
            var message = context.Message;

            string id = message.Id;
            string firstName = message.FirstName;
            string lastName = message.LastName;

            var appoitments = await _repositoryManager.AppoitmentRepository.GetAppoitmentsByDoctor(id, trackChanges: true);
            appoitments.ToList().ForEach(a =>
            {
                a.DoctorFirstName = firstName;
                a.DoctorLastName = lastName;
            });

            await _repositoryManager.SaveAsync();

        }
    }
}
