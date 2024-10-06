namespace LinkDev.Talabat.Core.Domain.Contract
{
    public interface IStoreContextInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
