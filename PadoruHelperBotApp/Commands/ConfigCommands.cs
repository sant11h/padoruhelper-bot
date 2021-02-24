using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PadoruHelperBotApp.Commands
{
    [Group("config")]
    public class ConfigCommands : BaseCommandModule
    {
        //[Command("work")]
        //public async Task ConfigWorks(CommandContext ctx)
        //{
        //    await ctx.Channel.SendMessageAsync($"The alert works are: { Repository.GetInstance().Subscriptions[ctx.Member.Id].Works}. " +
        //        $"True: enabled, false: disabled. Type true or false to change it.");

        //    var interactivity = ctx.Client.GetInteractivity();
        //    var msg = await interactivity.WaitForMessageAsync(
        //        x => x.Author == ctx.User
        //        && x.Channel == ctx.Channel)
        //        .ConfigureAwait(false);

        //    switch (msg.Result.Content)
        //    {
        //        case "true":
        //            {
        //                Repository.GetInstance().Subscriptions[ctx.Member.Id].Works = true;
        //                await ctx.Channel.SendMessageAsync($"Works will be alerted!");
        //            }
        //            break;
        //        case "false":
        //            {
        //                Repository.GetInstance().Subscriptions[ctx.Member.Id].Works = false;
        //                await ctx.Channel.SendMessageAsync($"Works won't be alerted!");
        //            }
        //            break;
        //        default:
        //            await ctx.Channel.SendMessageAsync($"Wrong syntax!");
        //            break;
        //    }
        //}
    }
}
