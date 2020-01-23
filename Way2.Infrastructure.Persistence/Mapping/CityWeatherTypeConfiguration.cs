using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Way2.Domain.Entities;

namespace Way2.Infrastructure.Persistence.Mapping
{
    public class CityWeatherTypeConfiguration : EntityTypeConfiguration<CityWeather>
    {
        public override void Configure(EntityTypeBuilder<CityWeather> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Date).HasConversion(
                v => v.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffzzz"),
                v => DateTimeOffset.Parse(v));
        }
    }
}
