using DSharpPlus;
using PadoruHelperBot.Core.Services;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PadoruHelperBotApp.Services.Alerts
{
    public class GuildStrategy : IAlertStrategy
    {
        public TimeSpan completionTime { get; private set; }
        public GuildStrategy(TimeSpan completionTimeSpan)
        {
            completionTime = completionTimeSpan;
        }

        public async Task Alert(DiscordClient client, IAlertsService alertService)
        {
            var alerts = await alertService.GetByType(AlertType.GuildRaid);

            foreach (var item in alerts)
            {
                if ((DateTime.Now - item.SendedAt) > completionTime)
                {
                    await alertService.Remove(item);

                    await client.GetGuildAsync(item.GuildId)
                    .Result.GetChannel(item.ChannelId)
                    .SendMessageAsync($"The team { client.GetGuildAsync(item.GuildId).Result.GetRole(815401607382171698).Mention} is ready to **raid**!");
                }
            }
        }

        public async Task RemoveExpired(DiscordClient client, IAlertsService alertService)
        {
            var alerts = await alertService.GetByType(AlertType.GuildRaid);

            foreach (var item in alerts)
            {
                if (DateTime.Now - item.SendedAt > completionTime)
                {
                    await alertService.Remove(item);
                }
            }
        }
    }
}
