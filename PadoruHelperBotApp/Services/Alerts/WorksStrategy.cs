using DSharpPlus;
using PadoruHelperBot.Core.Services.Alerts;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PadoruHelperBotApp.Services.Alerts
{
    public class WorksStrategy : IAlertStrategy
    {
        public async Task Alert(DiscordClient client, IAlertsService alertService)
        {
            var alerts = await alertService.GetByType(AlertType.Works);

            foreach (var item in alerts)
            {
                if ((DateTime.Now - item.SendedAt).TotalMinutes > 5)
                {
                    await alertService.Remove(item);

                    await client.GetGuildAsync(item.GuildId)
                    .Result.GetChannel(item.ChannelId)
                    .SendMessageAsync($"{ client.GetUserAsync(item.UserId).Result.Mention} work done!");
                }
            }
        }

        public async Task RemoveExpired(DiscordClient client, IAlertsService alertService)
        {
            var alerts = await alertService.GetByType(AlertType.Works);

            foreach (var item in alerts)
            {
                if ((DateTime.Now - item.SendedAt).TotalMinutes > 5)
                {
                    await alertService.Remove(item);
                }
            }
        }
    }
}
