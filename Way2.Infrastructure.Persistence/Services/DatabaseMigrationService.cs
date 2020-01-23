using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Way2.Application.Services;

namespace Way2.Infrastructure.Persistence.Services
{
    public class DatabaseMigrationService : IDatabaseMigrationService
    {

        private readonly ApplicationDbContext _context;

        public DatabaseMigrationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task MigrateAsync()
        {
            await _context.Database.MigrateAsync();
        }

    }
}
