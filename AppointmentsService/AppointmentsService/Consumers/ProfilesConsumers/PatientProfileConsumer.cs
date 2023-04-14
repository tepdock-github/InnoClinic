using Appoitments.Domain.Interfaces;
using MassTransit;
using SharedModelsInnoClinic;

namespace AppointmentsService.Consumers.ProfilesConsumers
{
    public class PatientProfileConsumer : IConsumer<IPatientProfileManipulation>
    {
        private readonly IRepositoryManager _repositoryManager;

        public PatientProfileConsumer(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task Consume(ConsumeContext<IPatientProfileManipulation> context)
        {
            var message = context.Message;

            Guid id = message.Id;
            string firstName = message.FirstName;
            string lastName = message.LastName;

            var appoitments = await _repositoryManager.AppoitmentRepository.GetAppoitmentsByPatient(id, trackChanges: true);
            appoitments.ToList().ForEach(a =>
            {
                a.PatientFirstName = firstName;
                a.PatientLastName = lastName;     
            });

            await _repositoryManager.SaveAsync();
        }
    }
}
