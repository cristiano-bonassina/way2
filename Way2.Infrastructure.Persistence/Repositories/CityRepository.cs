using System;
using Way2.Application.Repositories.Abstractions;
using Way2.Domain.Entities;

namespace Way2.Infrastructure.Persistence.Repositories
{
    public class CityRepository : Repository<City, Guid>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
