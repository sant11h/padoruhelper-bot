using DSharpPlus;
using DSharpPlus.CommandsNext;
using PadoruHelperBot.Core.Services.Alerts;
using PadoruHelperBotDAL;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PadoruHelperBotApp
{
    public class AlertNotifier : IAlertNotifier
    {
        private IAlertService _alertService;
        public AlertNotifier(IAlertService alertService)
        {
            _alertService = alertService;
        }

        public async Task AlertWork(DiscordClient client)
        {
            var alerts = new List<AlertPetition>();
            alerts = await _alertService.GetAlertPetitionsByType(AlertType.Works);

            foreach (var item in alerts)
            {
                if ((DateTime.Now - item.SendedAt).TotalMinutes > 5)
                {
                    await _alertService.RemoveAlertPetition(item);

                    await client.GetGuildAsync(item.GuildId)
                    .Result.GetChannel(item.ChannelId)
                    .SendMessageAsync($"{ client.GetUserAsync(item.UserId).Result.Mention} work done!");
                }
            }
        }
    }
}
