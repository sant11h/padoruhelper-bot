using DSharpPlus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PadoruHelperBot.Core.Services;
using PadoruHelperBotApp.Services.Alerts;
using PadoruHelperBotDAL;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PadoruHelperBotApp
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = _config.GetConnectionString("SantiServer");

            services.AddDbContext<PadoruHelperContext>(opt =>
            {
                opt.UseMySql(connectionString, 
                    new MySqlServerVersion(new System.Version(10,4,14)),
                    mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)
                    .MigrationsAssembly("PadoruHelperBotDAL.Migrations")
                   );
   
            });

            services.AddScoped<IAlertsService, AlertsService>();
            services.AddScoped<IUserSubscriptionsService, UserSubscriptionsService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAlertsController, AlertsController>();

            var serviceProvider = services.BuildServiceProvider();

            var bot = new Bot(serviceProvider, serviceProvider.GetRequiredService<IServiceScopeFactory>());
            services.AddSingleton(bot);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
