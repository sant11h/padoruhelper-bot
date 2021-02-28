using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.EntityFrameworkCore;
using PadoruHelperBot.Core.Services.Alerts;
using PadoruHelperBot.Core.Services.User;
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
        private readonly IAlertsService _alertService;
        private readonly IUserSubscriptionsService _userSubsService;
        public TimerCommands(IAlertsService alertService, IUserSubscriptionsService userSubsService)
        {
            _alertService = alertService;
            _userSubsService = userSubsService;

        }

        [Command("chop")]
        [Aliases(new string[] { "fish", "axe", "net", "bowsaw", "boat", "pickup",
                                "ladder", "mine", "pickaxe", "tractor", "chainsaw",
                                "bigboat", "drill", "greenhouse", "dynamite"})]
        [Hidden]
        public async Task Work(CommandContext ctx)
        {
            if (!await IsSubscribed(ctx, AlertType.Works))
            {
                return;
            }

            if (ctx.Prefix.ToLower() == "rpg")
            {
                var item = await _alertService.Get(ctx.Member.Id, AlertType.Works).ConfigureAwait(false);
                if (item == null)
                {
                    await _alertService.Add(
                        new AlertPetition
                        {
                            UserId = ctx.Member.Id,
                            AlertType = AlertType.Works,
                            GuildId = ctx.Guild.Id,
                            ChannelId = ctx.Channel.Id,
                            SendedAt = DateTime.Now
                        });
                }
            }
        }

        [Command("adventure")]
        [Aliases(new string[] { "adv" })]
        [Hidden]
        public async Task Adventure(CommandContext ctx)
        {
            if (!await IsSubscribed(ctx, AlertType.Adventure))
            {
                return;
            }

            if (ctx.Prefix.ToLower() == "rpg")
            {
                var item = await _alertService.Get(ctx.Member.Id, AlertType.Adventure).ConfigureAwait(false);
                if (item == null)
                {
                    await _alertService.Add(
                        new AlertPetition
                        {
                            UserId = ctx.Member.Id,
                            AlertType = AlertType.Adventure,
                            GuildId = ctx.Guild.Id,
                            ChannelId = ctx.Channel.Id,
                            SendedAt = DateTime.Now
                        });
                }
            }
        }

        [Command("training")]
        [Aliases(new string[] { "tr" })]
        [Hidden]
        public async Task Training(CommandContext ctx)
        {
            if (!await IsSubscribed(ctx, AlertType.Training))
            {
                return;
            }

            if (ctx.Prefix.ToLower() == "rpg")
            {
                var item = await _alertService.Get(ctx.Member.Id, AlertType.Training).ConfigureAwait(false);
                if (item == null)
                {
                    await _alertService.Add(
                        new AlertPetition
                        {
                            UserId = ctx.Member.Id,
                            AlertType = AlertType.Training,
                            GuildId = ctx.Guild.Id,
                            ChannelId = ctx.Channel.Id,
                            SendedAt = DateTime.Now
                        });
                }
            }
        }

        public async Task<bool> IsSubscribed(CommandContext ctx, AlertType alertType)
        {
            var userSubs = await _userSubsService.GetOrCreate(ctx.Member.Id, ctx.Guild.Id).ConfigureAwait(false);

            switch (alertType)
            {
                case AlertType.Works: return userSubs.Works;
                case AlertType.Adventure: return userSubs.Adventure;
                case AlertType.Training: return userSubs.Training;
                default: return false;
            }
        }
    }
}
