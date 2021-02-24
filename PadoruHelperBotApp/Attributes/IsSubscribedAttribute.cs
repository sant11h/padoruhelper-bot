using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PadoruHelperBotApp.Attributes
{
    public class IsSubscribedAttribute : CheckBaseAttribute
    {
        public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            throw new NotImplementedException();
            //if (ctx.Member.Id)
            //{

            //}
        }
    }
}
