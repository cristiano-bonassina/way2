using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Way2.Application.Services;

namespace Way2.Presentation.WebApi.Services
{
    /// <summary>
    /// This service applies database migrations
    /// </summary>
    internal class DatabaseMigrationHostedService : IHostedService
    {

        private readonly IDatabaseMigrationService _databaseMigrationService;

        public DatabaseMigrationHostedService(IDatabaseMigrationService databaseMigrationService)
        {
            _databaseMigrationService = databaseMigrationService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _databaseMigrationService.MigrateAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}
