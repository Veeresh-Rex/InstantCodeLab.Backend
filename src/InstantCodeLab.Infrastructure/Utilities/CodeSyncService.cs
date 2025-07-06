using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InstantCodeLab.Domain.Entities;
using InstantCodeLab.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace InstantCodeLab.Infrastructure.Utilities;

public class CodeSyncService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CodeStore _codeStore;
    private readonly ILogger<CodeSyncService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(100);

    public CodeSyncService(IServiceProvider serviceProvider, CodeStore codeStore, ILogger<CodeSyncService> logger)
    {
        _serviceProvider = serviceProvider;
        _codeStore = codeStore;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("CodeSyncService started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                try
                {
                    var allCodes = _codeStore.GetAllCodes();

                    var bulkOps = allCodes.Select(entry =>
                        new UpdateOneModel<User>(
                            Builders<User>.Filter.Eq(u => u._id, entry.Key),
                            Builders<User>.Update.Set(u => u.OwnCode, entry.Value)
                    )
                    ).ToList();

                    await userRepository.BulkWriteAsync(bulkOps);

                    foreach (var userCode in allCodes)
                    {
                        _codeStore.RemoveCode(userCode.Key);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while syncing code data.");
                }
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}
