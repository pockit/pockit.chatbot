using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pockit.ChatBot.Api.Services;
using Pockit.ChatBot.Api.Options;
using Pockit.ChatBot.Api.Providers;
using Pockit.ChatBot.Api.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Pockit.ChatBot.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Get auth tokens
            var reportBotAppToken = Configuration.GetSection("Authentication").GetValue<string>("ReportBotAppToken");

            // Configure authentication
            services.AddAuthorization(options =>
                options.AddPolicy(
                    "IsReportBotRequest", 
                    policy => policy.Requirements.Add(new SlackAppTokenRequirement(reportBotAppToken))));

            // Define services for DI
            services.AddSingleton<IStorageService, AzureStorageService>();
            services.AddSingleton<ISlackReportBotProvider, SlackReportBotProvider>();
            services.AddSingleton<IAuthorizationHandler, SlackAppTokenHandler>();

            // Define config sections
            services.Configure<StorageOptions>(Configuration.GetSection("Storage"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
