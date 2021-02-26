using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using PadoruHelperBot.Core.Services.User;
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
        public ConfigCommands(IUserSubscriptionsService userSubsService)
        {
            _userSubsService = userSubsService;
        }

        [GroupCommand]
        public async Task Config(CommandContext ctx)
        {
            var warningEmoji = DiscordEmoji.FromName(ctx.Client, ":warning:");
            var axeEmoji = DiscordEmoji.FromName(ctx.Client, ":axe:");
            var arrowEmoji = DiscordEmoji.FromName(ctx.Client, ":arrow_double_up:");
            var boomEmoji = DiscordEmoji.FromName(ctx.Client, ":boom:");

            var embed = new DiscordEmbedBuilder
            {
                Title = $"{warningEmoji} Configuration for Alerts {warningEmoji}",
                Color = DiscordColor.Aquamarine
            };
            embed.AddField($"{axeEmoji} config work {axeEmoji}", "Enable or disable **works** (chop, fish, etc) alert.");
            embed.AddField($"{arrowEmoji} config training {arrowEmoji}", "Enable or disable **training** alert.");
            embed.AddField($"{boomEmoji} config adventure {boomEmoji}", "Enable or disable **adventure** alert.");

            await ctx.Channel.SendMessageAsync(embed);
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
    }
}
