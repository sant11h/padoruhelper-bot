using DSharpPlus;
using System.Threading.Tasks;

namespace PadoruHelperBotApp
{
    public interface IAlertNotifier
    {
        Task AlertWork(DiscordClient client);
    }
}