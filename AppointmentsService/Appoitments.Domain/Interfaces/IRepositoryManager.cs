namespace Appoitments.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IAppoitmentRepository AppoitmentRepository { get; }
        IResultRepository ResultRepository { get; }
        IScheduleRepository ScheduleRepository { get; }
        Task SaveAsync();
    }
}
