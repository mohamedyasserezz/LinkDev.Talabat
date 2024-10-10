namespace LinkDev.Talabat.Core.Domain.Contract.Persistance
{
    public interface IStoreContextInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
