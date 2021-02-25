using DSharpPlus;
using System.Threading.Tasks;

namespace PadoruHelperBotApp.Services.Alerts
{
    public interface IAlertsController
    {
        void SetStrategy(IAlertStrategy strategy);
        Task AlertWork(DiscordClient client);
        Task RemoveExpiredAlerts(DiscordClient client);
    }
}