using FastCore.DbMigration;
using FastCore.EFCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FastCore.DbMigrations
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        public DbMigratorHostedService(IServiceProvider serviceProvider, ILogger<DbMigratorHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var context = _serviceProvider.GetService(typeof(FastCoreContext));

            var fcContext = context as FastCoreContext;
            await fcContext.Database.MigrateAsync(cancellationToken: cancellationToken);

            DbInitializer.Seed(fcContext);
            _logger.LogInformation("迁移完成！！！按任意键继续...");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
