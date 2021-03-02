using DSharpPlus;
using PadoruHelperBot.Core.Services;
using System.Threading.Tasks;

namespace PadoruHelperBotApp.Services.Alerts
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
