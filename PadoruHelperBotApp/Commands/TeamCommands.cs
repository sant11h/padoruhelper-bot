using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using PadoruHelperBot.Core.Services;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PadoruHelperBotApp.Commands
{
    [Group("guild")]
    public class TeamCommands : BaseCommandModule
    {
        private readonly IAlertsService _alertService;
        private readonly IUserSubscriptionsService _userSubsService;
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;
        public TeamCommands(IAlertsService alertService, IUserSubscriptionsService userSubsService,
            ITeamService teamService, IUserService userService)
        {
            _alertService = alertService;
            _userSubsService = userSubsService;
            _teamService = teamService;
            _userService = userService;
        }

        [Command("join")]
        [Description("Joins THE team")]
        public async Task Join(CommandContext ctx)
        {
            if (ctx.Prefix != "|")
            {
                return;
            }

            var joinEmbed = new DiscordEmbedBuilder
            {
                Title = "Would you like to join THE team?",
                Color = DiscordColor.Green,

            };

            var joinMsg = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);
            var yEmoji = DiscordEmoji.FromName(ctx.Client, ":regional_indicator_y:");
            var nEmoji = DiscordEmoji.FromName(ctx.Client, ":regional_indicator_n:");

            await joinMsg.CreateReactionAsync(yEmoji).ConfigureAwait(false);
            await joinMsg.CreateReactionAsync(nEmoji).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

            var result = await interactivity.WaitForReactionAsync(
                    x => x.Message == joinMsg &&
                    x.User.Id == ctx.Member.Id &&
                    (x.Emoji == yEmoji || x.Emoji == nEmoji)).ConfigureAwait(false);

            if (result.Result.Emoji == yEmoji)
            {
                var team = await _teamService.GetByName("EpicRPG").ConfigureAwait(false);

                var role = ctx.Guild.Roles[team.RoleId];

                await _userService.UpdateTeam(ctx.User.Id, team.Id);

                await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
            }
            else if (result.Result.Emoji == nEmoji)
            {

            }
        }

        [Command("raid")]
        [Aliases(new string[] { "upgrade" })]
        [Hidden]
        public async Task Upgrade(CommandContext ctx)
        {
            if (ctx.Prefix.ToLower() != "rpg")
            {
                return;
            }

            var team = await _userService.GetTeam(ctx.Member.Id).ConfigureAwait(false);
            if (team == null)
            {
                await ctx.Channel.SendMessageAsync("You won't be alerted because you don't belong to a team.").ConfigureAwait(false);
                return;
            }

            var item = await _alertService.Get(team.Id, AlertType.GuildRaid).ConfigureAwait(false);
            if (item == null)
            {
                await _alertService.Add(
                    new AlertPetition
                    {
                        UserId = team.Id,
                        AlertType = AlertType.GuildRaid,
                        GuildId = ctx.Guild.Id,
                        ChannelId = ctx.Channel.Id,
                        SendedAt = DateTime.Now
                    });
            }

        }
    }
}
