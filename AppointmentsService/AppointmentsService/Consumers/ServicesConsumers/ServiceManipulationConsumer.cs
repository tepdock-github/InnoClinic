using Appoitments.Domain.Interfaces;
using MassTransit;
using SharedModelsInnoClinic;

namespace AppointmentsService.Consumers.ServicesConsumers
{
    public class ServiceManipulationConsumer : IConsumer<IServiceManipulation>
    {
        private readonly IRepositoryManager _repositoryManager;

        public ServiceManipulationConsumer(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task Consume(ConsumeContext<IServiceManipulation> context)
        {
            var message = context.Message;

            int id = message.Id;
            string serviceName = message.ServiceName;

            var appoitments = await _repositoryManager.AppoitmentRepository.GetAppoitmentsByServiceId(id, trackChanges: true);
            appoitments.ToList().ForEach(a =>
            {
                a.ServiceName = serviceName;
            });

            await _repositoryManager.SaveAsync();
        }
    }
}
