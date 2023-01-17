namespace ServicesService.ServicesInterfaces
{
    public interface IServicesManager
    {
        ICategoryServices CategoryServices { get; }
        IServiceServices ServiceServices { get; }
        ISpecializationServices SpecializationServices { get; }
    }
}
