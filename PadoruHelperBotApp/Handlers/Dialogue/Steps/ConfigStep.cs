using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PadoruHelperBotApp.Handlers.Dialogue.Steps
{
    public class ConfigStep:DialogueStepBase
    {
        private IDialogueStep _nextStep;

        public ConfigStep(string content, IDialogueStep nextStep) : base(content)
        {
            _nextStep = nextStep;
        }

        public Action<string> OnValidResult { get; set; } = delegate { };

        public override IDialogueStep NextStep => _nextStep;

        public override async Task<bool> ProcessStep(DiscordClient client, DiscordChannel channel, DiscordUser user)
        {
            var embedBuilder = new DiscordEmbedBuilder
            {
                Title = "Please respond below",
                Description = $"{user.Mention}, {_content}"
            };

            embedBuilder.AddField("Answer with: ", "yes/no/y/n");
            embedBuilder.AddField("To stop the dialogue: ", "Use the |cancel command");

            var interactivity = client.GetInteractivity();
            while (true)
            {
                var embed = await channel.SendMessageAsync(embed: embedBuilder).ConfigureAwait(false);

                OnMessageAdded(embed);

                var messageResult = await interactivity.WaitForMessageAsync(
                    x => x.ChannelId == channel.Id && x.Author.Id == user.Id)
                    .ConfigureAwait(false);

                OnMessageAdded(messageResult.Result);

                if (messageResult.TimedOut == true || messageResult.Result.Content.Equals("|cancel", StringComparison.OrdinalIgnoreCase)) 
                {
                    return true;
                }

                if (!(messageResult.Result.Content.Equals("yes", StringComparison.OrdinalIgnoreCase)
                    || messageResult.Result.Content.Equals("no", StringComparison.OrdinalIgnoreCase)
                    || messageResult.Result.Content.Equals("y", StringComparison.OrdinalIgnoreCase)
                    || messageResult.Result.Content.Equals("n", StringComparison.OrdinalIgnoreCase)))
                {
                    await TryAgain(channel, $"Your input is not correct");
                    continue;
                }

                OnValidResult(messageResult.Result.Content.ToLower());

                return false;
            }
        }
    }
}
