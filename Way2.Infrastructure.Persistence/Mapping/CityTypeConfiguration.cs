using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Way2.Domain.Entities;

namespace Way2.Infrastructure.Persistence.Mapping
{
    public class CityTypeConfiguration : EntityTypeConfiguration<City>
    {
        public override void Configure(EntityTypeBuilder<City> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasColumnType("TEXT COLLATE LATIN1_GENERAL_CS_AI").IsRequired();
            builder.HasIndex(x => x.Name);
        }
    }
}
