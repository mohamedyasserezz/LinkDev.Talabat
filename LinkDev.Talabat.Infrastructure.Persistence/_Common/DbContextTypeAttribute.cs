namespace LinkDev.Talabat.Infrastructure.Persistence.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbContextTypeAttribute : Attribute
    {
        public DbContextTypeAttribute(Type dbContextType)
        {
            DbContextType = dbContextType;
        }

        public Type DbContextType { get; }
    }
}
