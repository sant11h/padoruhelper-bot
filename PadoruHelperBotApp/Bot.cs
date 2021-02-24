using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PadoruHelperBotApp.Commands;
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
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public IAlertNotifier _alertNotifier { get; set; }
        private Timer timer;

        public Bot(IServiceProvider services, IServiceScopeFactory scopeFactory)
        {
            _serviceScopeFactory = scopeFactory;

            InitConfiguration(services);

            InitCommands();

            Client.ConnectAsync();

            InitTimer();
        }

        public void Test()
        {
            Client.GetGuildAsync(90902701063282688)
                    .Result.GetChannel(813165215436505138)
                    .SendMessageAsync($"test");
        }

        private void InitConfiguration(IServiceProvider services)
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
                Timeout = TimeSpan.FromSeconds(10)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = configJson.Prefixes,
                EnableDms = false,
                EnableMentionPrefix = true,
                Services = services
            };

            Commands = Client.UseCommandsNext(commandsConfig);

           
        }

        private void InitCommands()
        {
            Commands.RegisterCommands<FunCommands>();
            Commands.RegisterCommands<TimerCommands>();
            //Commands.RegisterCommands<ConfigCommands>();
        }

        private Task Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        private void InitTimer()
        {
            timer = new Timer();
            timer.Interval = 5000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetService<IAlertNotifier>();
                await scopedService.AlertWork(Client);
            }
        }
    }
}
