
using AFG_New_passport_Website.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace AFG_New_passport_Website.Models.Domain
{
    public class BlockCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public BlockCleanupService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<WebDbContext>();

                var expiredBlocks = context.SearchBlocks
                    .Where(b => b.BlockedUntil != null && b.BlockedUntil <= DateTime.UtcNow);

                if (expiredBlocks.Any())
                {
                    context.SearchBlocks.RemoveRange(expiredBlocks);
                    await context.SaveChangesAsync();
                }

                //Run after 6 hours
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
