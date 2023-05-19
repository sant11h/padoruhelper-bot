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
    [Group("config")]
    [Description("Configuration for alerts")]
    public class ConfigCommands : BaseCommandModule
    {
        private readonly IUserSubscriptionsService _userSubsService;
        private readonly IUserService _userService;
        public ConfigCommands(IUserSubscriptionsService userSubsService, IUserService userService)
        {
            _userSubsService = userSubsService;
            _userService = userService;
        }

        [GroupCommand]
        public async Task Config(CommandContext ctx)
        {
            await GetConfigToDisplay(ctx, ctx.Member.Id);
        }

        [GroupCommand]
        public async Task Config(CommandContext ctx, DiscordMember member)
        {
            await GetConfigToDisplay(ctx, member.Id);
        }

        [Command("work")]
        [Description("Toggle work alert")]
        public async Task ConfigWorks(CommandContext ctx)
        {
            var sadgeEmoji = DiscordEmoji.FromName(ctx.Client, ":Sadge:");
            var usagiEmoji = DiscordEmoji.FromName(ctx.Client, ":usagi:");

            var inputStep = new ConfigStep("Should I notify you when your **work** is done?", null);

            string input = string.Empty;

            inputStep.OnValidResult += (result) => input = result;

            var inputDialogHandler = new DialogueHandler(ctx.Client, ctx.Channel, ctx.User, inputStep);

            bool succeeded = await inputDialogHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            if (input.Equals("yes") || input.Equals("y"))
            {
                await UpdateState(ctx, AlertType.Works, true).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync($"{ctx.Member.Mention}, your **works** alerts are enabled! {usagiEmoji}").ConfigureAwait(false);
            }
            else
            {
                await UpdateState(ctx, AlertType.Works, false).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync($"{ctx.Member.Mention}, your **works** alerts are disabled! {sadgeEmoji}").ConfigureAwait(false);
            }
        }

        [Command("training")]
        [Description("Toggle training alert")]
        public async Task ConfigTraining(CommandContext ctx)
        {
            var sadgeEmoji = DiscordEmoji.FromName(ctx.Client, ":Sadge:");
            var usagiEmoji = DiscordEmoji.FromName(ctx.Client, ":usagi:");

            var inputStep = new ConfigStep("Should I notify you when your **training** is done?", null);

            string input = string.Empty;

            inputStep.OnValidResult += (result) => input = result;

            var inputDialogHandler = new DialogueHandler(ctx.Client, ctx.Channel, ctx.User, inputStep);

            bool succeeded = await inputDialogHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            if (input.Equals("yes") || input.Equals("y"))
            {
                await UpdateState(ctx, AlertType.Training, true).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync($"{ctx.Member.Mention}, your **training** alerts are enabled! {usagiEmoji}").ConfigureAwait(false);
            }
            else
            {
                await UpdateState(ctx, AlertType.Training, false).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync($"{ctx.Member.Mention}, your *training** alerts are disabled! {sadgeEmoji}").ConfigureAwait(false);
            }
        }

        [Command("adventure")]
        [Description("Toggle adventure alert")]
        public async Task ConfigAdventure(CommandContext ctx)
        {
            var sadgeEmoji = DiscordEmoji.FromName(ctx.Client, ":Sadge:");
            var usagiEmoji = DiscordEmoji.FromName(ctx.Client, ":usagi:");

            var inputStep = new ConfigStep("Should I notify you when your **adventure** is done?", null);

            string input = string.Empty;

            inputStep.OnValidResult += (result) => input = result;

            var inputDialogHandler = new DialogueHandler(ctx.Client, ctx.Channel, ctx.User, inputStep);

            bool succeeded = await inputDialogHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            if (input.Equals("yes") || input.Equals("y"))
            {
                await UpdateState(ctx, AlertType.Adventure, true).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync($"{ctx.Member.Mention}, your **adventure** alerts are enabled! {usagiEmoji}").ConfigureAwait(false);
            }
            else
            {
                await UpdateState(ctx, AlertType.Adventure, false).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync($"{ctx.Member.Mention}, your *adventure** alerts are disabled! {sadgeEmoji}").ConfigureAwait(false);
            }
        }

        private async Task UpdateState(CommandContext ctx, AlertType alertType, bool isSubscribed)
        {
            var userSubs = await _userSubsService.GetOrCreate(ctx.Member.Id, ctx.Guild.Id).ConfigureAwait(false);

            switch (alertType)
            {
                case AlertType.Works: userSubs.Works = isSubscribed;
                    break;
                case AlertType.Adventure: userSubs.Adventure = isSubscribed;
                    break;
                case AlertType.Training: userSubs.Training = isSubscribed;
                    break;
            }

            await _userSubsService.Update(userSubs).ConfigureAwait(false);
        }

        private async Task GetConfigToDisplay(CommandContext ctx, ulong memberId)
         {
            var userData = await _userSubsService.GetOrCreate(memberId, ctx.Guild.Id).ConfigureAwait(false);
            DiscordMember member = await ctx.Guild.GetMemberAsync(memberId).ConfigureAwait(false);
            
            var axeEmoji = DiscordEmoji.FromName(ctx.Client, ":axe:");
            var arrowEmoji = DiscordEmoji.FromName(ctx.Client, ":arrow_double_up:");
            var boomEmoji = DiscordEmoji.FromName(ctx.Client, ":boom:");


            var embed = new DiscordEmbedBuilder
            {
                Title = $"{member.DisplayName}'s configuration profile",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = member.AvatarUrl },
                Description = await TeamString(memberId).ConfigureAwait(false),
                Color = DiscordColor.Aquamarine
            };
            embed.AddField($"{axeEmoji} config work {axeEmoji}", $"Enable or disable **work** (chop, fish, etc) alert. Currently is **{BoolToText(userData.Works)}**.");
            embed.AddField($"{arrowEmoji} config training {arrowEmoji}", $"Enable or disable **training** alert. Currently is **{BoolToText(userData.Training)}**.");
            embed.AddField($"{boomEmoji} config adventure {boomEmoji}", $"Enable or disable **adventure** alert. Currently is **{BoolToText(userData.Adventure)}**.");

            await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
        }

        private async Task<string> TeamString(ulong userId)
        {
            var userTeam = await _userService.GetTeam(userId).ConfigureAwait(false);

            if (userTeam == null)
            {
                return "Team: -";
            }

            return $"Team: **{userTeam.Name}**";
        }

        private string BoolToText(bool boolean)
        {
            switch (boolean)
            {
                case true: return "enabled";
                case false: return "disabled";
            }
        }
    }
}
