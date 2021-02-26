using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DSharpPlus;
using PadoruHelperBotApp.Services.Alerts;

namespace PadoruHelperBotApp.Services
{
    public class BotHelper
    {
        private Timer timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly DiscordClient _client;
        private List<IAlertStrategy> strategies;

        public BotHelper(DiscordClient client, IServiceScopeFactory serviceScopeFactory)
        {
            _client = client;
            _serviceScopeFactory = serviceScopeFactory;

            InitStrategies();
        }

        private void InitStrategies()
        {
            strategies = new List<IAlertStrategy>
            {
                new WorksStrategy(TimeSpan.FromMinutes(5)),
                new TrainingStrategy(TimeSpan.FromMinutes(15)),
                new AdventureStrategy(TimeSpan.FromHours(1))
            };
        }

        public void InitAlertsLoop()
        {
            timer = new Timer();
            timer.Interval = 5000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
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

        public async void RemoveExpiredAlerts()
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
    }
}
