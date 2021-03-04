using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using PadoruHelperBot.Core.Services;
using PadoruHelperBotApp.Handlers.Dialogue;
using PadoruHelperBotApp.Handlers.Dialogue.Steps;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PadoruHelperBotApp.Commands
{
    [Group("buy")]
    [Description("Alerts for buy commands")]
    public class BuyCommands:BaseCommandModule
    {
        private readonly IAlertsService _alertService;
        private readonly IUserSubscriptionsService _userSubsService;
        public BuyCommands(IAlertsService alertService, IUserSubscriptionsService userSubsService)
        {
            _alertService = alertService;
            _userSubsService = userSubsService;
        }


        [Command("common")]
        [Aliases(new string[] { "uncommon", "rare", "epic", "edgy" })]

        [Hidden]
        public async Task Buy(CommandContext ctx)
        {
            string messasge = ctx.Message.Content.ToLower();
            if (ctx.Prefix.ToLower() != "rpg" || !messasge.Contains("lootbox"))
            {
                return;
            }

            var item = await _alertService.Get(ctx.Member.Id, AlertType.BuyLootbox).ConfigureAwait(false);
            if (item == null)
            {
                await _alertService.Add(
                    new AlertPetition
                    {
                        UserId = ctx.Member.Id,
                        AlertType = AlertType.BuyLootbox,
                        GuildId = ctx.Guild.Id,
                        ChannelId = ctx.Channel.Id,
                        SendedAt = DateTime.Now
                    });
            }
        }

        [Command("remove")]
        public async Task Remove(CommandContext ctx)
        {
            var item = await _alertService.Get(ctx.Member.Id, AlertType.BuyLootbox).ConfigureAwait(false);

            if (item != null)
            {
                await _alertService.Remove(item);
                await ctx.Channel.SendMessageAsync("Your alert for **lootboxes** has been removed!");
            }
            else
            {
                await ctx.Channel.SendMessageAsync("You don't have a pending lootbox alert!");
            }
        }
    }
}
