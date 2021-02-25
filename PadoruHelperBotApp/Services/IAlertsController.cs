using DSharpPlus;
using PadoruHelperBotApp.Services.Alerts;
using System.Threading.Tasks;

namespace PadoruHelperBotApp
{
    public interface IAlertsController
    {
        void SetStrategy(IAlertStrategy strategy);
        Task AlertWork(DiscordClient client);
        Task RemoveExpiredAlerts(DiscordClient client);
    }
}