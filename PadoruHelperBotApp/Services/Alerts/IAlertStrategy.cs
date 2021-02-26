using DSharpPlus;
using PadoruHelperBot.Core.Services.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PadoruHelperBotApp.Services.Alerts
{
    public interface IAlertStrategy
    {
        Task RemoveExpired(DiscordClient client, IAlertsService alertService);
        Task Alert(DiscordClient client, IAlertsService alertService);
        TimeSpan completionTime { get; }
    }
}
