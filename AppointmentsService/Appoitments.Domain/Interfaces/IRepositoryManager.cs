namespace Appoitments.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IAppoitmentRepository AppoitmentRepository { get; }
        IResultRepository ResultRepository { get; }
        Task SaveAsync();
    }
}
