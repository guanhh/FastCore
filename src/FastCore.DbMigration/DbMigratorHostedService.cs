using FastCore.EFCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FastCore.DbMigrations
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DbMigratorHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var context = _serviceProvider.GetService(typeof(FastCoreContext));
            await (context as FastCoreContext).Database.MigrateAsync(cancellationToken: cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
