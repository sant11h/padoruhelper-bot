using Microsoft.Extensions.DependencyInjection;
using PadoruHelperBot.Core.Services;
using PadoruHelperBotApp;
using PadoruHelperBotApp.Services;
using PadoruHelperBotApp.Services.Alerts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadoruHelperBot.Core.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAlertsService, AlertsService>();
            services.AddScoped<IUserSubscriptionsService, UserSubscriptionsService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAlertsController, AlertsController>();

            var serviceProvider = services.BuildServiceProvider();

            var bot = new Bot(serviceProvider, serviceProvider.GetRequiredService<IServiceScopeFactory>());
            services.AddSingleton(bot);

            services.AddSingleton(bot.Client);
            services.AddHostedService<EpicRpgHelper>();
        }
    }
}