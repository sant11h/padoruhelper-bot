using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.EntityFrameworkCore;
using PadoruHelperBot.Core.Services.Alerts;
using PadoruHelperBotDAL;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PadoruHelperBotApp.Commands
{
    public class TimerCommands : BaseCommandModule
    {
        private readonly IAlertService _alertService;
        public TimerCommands(IAlertService alertService)
        {
            _alertService = alertService;
        } 

        [Command("chop")]
        [Aliases(new string[] {"fish", "pickup", "mine"})]
        public async Task Work(CommandContext ctx)
        {
            if (ctx.Prefix.ToLower() == "rpg")
            {
                var item = await _alertService.GetAlertPetition(ctx.Member.Id, AlertType.Works).ConfigureAwait(false);
                if (item == null)
                {
                    await AddWork(ctx);
                }
            }
        }

        private async Task AddWork(CommandContext ctx)
        {
            await _alertService.AddAlertPetition(
                new AlertPetition
                {
                    UserId= ctx.Member.Id,
                    AlertType=AlertType.Works,
                    GuildId = ctx.Guild.Id,
                    ChannelId = ctx.Channel.Id,
                    SendedAt = DateTime.Now
                }).ConfigureAwait(false);
        }
    }
}
