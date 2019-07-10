using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.CloudFoundry.Connector.MySql.EFCore;

namespace PalTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
              services.AddSingleton(sp => new WelcomeMessage(
              Configuration.GetValue<string>("WELCOME_MESSAGE", "WELCOME_MESSAGE not configured.")
           ));
           services.AddSingleton(sp => new CloudFoundryInfo(
              Configuration.GetValue<string>("PORT", "port unknown"),
               Configuration.GetValue<string>("MEMORY_LIMIT", "memory limit unknown"),
               Configuration.GetValue<string>("CF_INSTANCE_INDEX", "cf instance index unknown"),
               Configuration.GetValue<string>("CF_INSTANCE_ADDR", "cf instance addr unknown")
           ));
         

           services.AddScoped<ITimeEntryRepository, MySqlTimeEntryRepository>();
           services.AddDbContext<TimeEntryContext>(options => options.UseMySql(Configuration));
         //services.AddSingleton<ITimeEntryRepository, InMemoryTimeEntryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
