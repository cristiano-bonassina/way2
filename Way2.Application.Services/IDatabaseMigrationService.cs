using System.Threading.Tasks;

namespace Way2.Application.Services
{
    public interface IDatabaseMigrationService
    {
        Task MigrateAsync();
    }
}
