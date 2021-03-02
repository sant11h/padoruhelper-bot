using DSharpPlus;
using PadoruHelperBot.Core.Services;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PadoruHelperBotApp.Services.Alerts
{
    public class TrainingStrategy : IAlertStrategy
    {
        public TimeSpan completionTime { get; private set; }

        public TrainingStrategy(TimeSpan completionTimeSpan)
        {
            completionTime = completionTimeSpan;
        }

        public async Task Alert(DiscordClient client, IAlertsService alertService)
        {
            var alerts = await alertService.GetByType(AlertType.Training);

            foreach (var item in alerts)
            {
                if ((DateTime.Now - item.SendedAt) > completionTime)
                {
                    await alertService.Remove(item);

                    await client.GetGuildAsync(item.GuildId)
                    .Result.GetChannel(item.ChannelId)
                    .SendMessageAsync($"{ client.GetUserAsync(item.UserId).Result.Mention}, your **training** is done!");
                }
            }
        }

        public async Task RemoveExpired(DiscordClient client, IAlertsService alertService)
        {
            var alerts = await alertService.GetByType(AlertType.Training);

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
