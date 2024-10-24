using LinkDev.Talabat.Core.Domain.Entities.Identity;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity.Config
{
    public class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(A => A.Id).ValueGeneratedOnAdd();
            builder.Property(nameof(Address.FirstName)).HasColumnType("nVarChar").HasMaxLength(50);
            builder.Property(nameof(Address.LastName)).HasColumnType("nVarChar").HasMaxLength(50);
            builder.Property(nameof(Address.Street)).HasColumnType("nVarChar").HasMaxLength(50);
            builder.Property(nameof(Address.City)).HasColumnType("nVarChar").HasMaxLength(50);
            builder.Property(nameof(Address.Country)).HasColumnType("nVarChar").HasMaxLength(50);
            builder.ToTable("Addresses");
        }
    }
}
