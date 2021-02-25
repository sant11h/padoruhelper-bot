using DSharpPlus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PadoruHelperBot.Core.Services.Alerts;
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
        public void ConfigureServices(IServiceCollection services)
        {
            string connString = "server=localhost;User Id = sant11h;Persist Security Info = True;database = padoruhelper;" +
                             "password = sundownss;Convert Zero Datetime = True;";

            services.AddDbContext<PadoruHelperContext>(opt =>
            {
                opt.UseMySql(connString, 
                    new MySqlServerVersion(new System.Version(10,4,14)),
                    mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)
                    .MigrationsAssembly("PadoruHelperBotDAL.Migrations")
                   );
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IAlertsService, AlertsService>();
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
