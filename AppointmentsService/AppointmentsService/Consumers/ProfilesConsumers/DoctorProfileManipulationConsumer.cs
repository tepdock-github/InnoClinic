using MassTransit;
using SharedModels.Interfaces;

namespace AppointmentsService.Consumers.ProfilesConsumers
{
    public class DoctorProfileManipulationConsumer : IConsumer<IDoctorProfileManipulation>
    {

        public async Task Consume(ConsumeContext<IDoctorProfileManipulation> context)
        {
            var message = context.Message;

            int id = message.Id;
            string firstName = message.FirstName;
            string lastName = message.LastName;
            string middleName = message.MiddleName;

            await Task.CompletedTask;

        }
    }
}
