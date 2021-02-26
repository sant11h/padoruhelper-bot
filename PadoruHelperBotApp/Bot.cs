using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PadoruHelperBotApp.Commands;
using PadoruHelperBotApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PadoruHelperBotApp
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        private BotHelper botHelper;

        public Bot(IServiceProvider services, IServiceScopeFactory scopeFactory)
        {
            InitConfiguration(services, scopeFactory);

            RegisterCommands();

            Client.ConnectAsync();
        }

        private void InitConfiguration(IServiceProvider services, IServiceScopeFactory scopeFactory)
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            {
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    json = sr.ReadToEnd();
                }
            }

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug
            };

            Client = new DiscordClient(config);

            Client.Ready += Client_Ready;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromSeconds(30)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = configJson.Prefixes,
                EnableDms = false,
                EnableMentionPrefix = true,
                Services = services
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            botHelper = new BotHelper(Client, scopeFactory);
            botHelper.RemoveExpiredAlerts();
            botHelper.InitAlertsLoop();
        }

        private void RegisterCommands()
        {
            //Commands.RegisterCommands<FunCommands>();
            Commands.RegisterCommands<TimerCommands>();
            Commands.RegisterCommands<ConfigCommands>();
        }

        private Task Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
