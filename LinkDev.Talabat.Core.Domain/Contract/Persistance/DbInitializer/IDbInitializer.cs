namespace LinkDev.Talabat.Core.Domain.Contract.Persistance.DbInitializer
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
