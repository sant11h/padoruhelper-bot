using DSharpPlus;
using DSharpPlus.CommandsNext;
using PadoruHelperBot.Core.Services.Alerts;
using PadoruHelperBotApp.Services.Alerts;
using PadoruHelperBotDAL;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PadoruHelperBotApp
{
    public class AlertsController : IAlertsController
    {
        private readonly IAlertsService _alertService;
        private IAlertStrategy _strategy;
        public AlertsController(IAlertsService alertService)
        {
            _alertService = alertService;
        }
        public void SetStrategy(IAlertStrategy strategy)
        {
            _strategy = strategy;
        }

        public async Task AlertWork(DiscordClient client)
        {
            await _strategy.Alert(client, _alertService);
        }

        public async Task RemoveExpiredAlerts(DiscordClient client)
        {
            await _strategy.RemoveExpired(client, _alertService);
        }
    }
}
