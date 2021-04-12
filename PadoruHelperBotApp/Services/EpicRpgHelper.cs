using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DSharpPlus;
using PadoruHelperBotApp.Services.Alerts;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace PadoruHelperBotApp.Services
{
    public class EpicRpgHelper : IHostedService
    {
        private Timer timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<EpicRpgHelper> _logger;
        private readonly DiscordClient _client;
        private List<IAlertStrategy> strategies;

        public EpicRpgHelper(DiscordClient client, IServiceScopeFactory serviceScopeFactory, ILogger<EpicRpgHelper> logger)
        {
            _client = client;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            InitStrategies();
        }

        private void InitStrategies()
        {
            strategies = new List<IAlertStrategy>
            {
                new WorksStrategy(TimeSpan.FromMinutes(5)),
                new TrainingStrategy(TimeSpan.FromMinutes(15)),
                new AdventureStrategy(TimeSpan.FromHours(1)),
                new GuildStrategy(TimeSpan.FromHours(2)),
                new BuyStrategy(TimeSpan.FromHours(3))
            };
        }

        private async void RemoveExpiredAlerts()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var alertService = scope.ServiceProvider.GetService<IAlertsController>();

                foreach (var strategy in strategies)
                {
                    alertService.SetStrategy(strategy);
                    await alertService.RemoveExpiredAlerts(_client);
                }
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Epic RPG Helper has started");

            RemoveExpiredAlerts();

            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            _logger.LogInformation("Im checking for alerts!");

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var alertService = scope.ServiceProvider.GetService<IAlertsController>();

                foreach (var strategy in strategies)
                {
                    alertService.SetStrategy(strategy);
                    await alertService.AlertWork(_client);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Epic RPG Helper has stopped");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
