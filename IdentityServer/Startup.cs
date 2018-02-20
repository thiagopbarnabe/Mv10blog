using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4;
using IdentityServer4.Stores;
using Microsoft.ApplicationInsights.Extensibility;
using IdentityServer.Persistence;
using Microsoft.AspNetCore.Authorization;

namespace IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(ConfigureIdentityServer.GetClients())
                .AddInMemoryIdentityResources(ConfigureIdentityServer.GetIdentityResources())
                .AddProfileService<UserProfileService>();

            services.AddSingleton<IUserStore, UserStore>();
            services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();

            services.AddAuthentication(options => { options.DefaultChallengeScheme = "Google"; })
                .AddGoogle("Google", options =>
                 {
                     options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                     options.ClientId = "931441181996-mkjk82ok1m9a4eqde6tfkmpv8kqj7o4v.apps.googleusercontent.com";
                     options.ClientSecret = "twubokDw9XV_91dnmS0TVUNT";

                 });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                try
                {
                    var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
                    configuration.DisableTelemetry = true;
                }
                catch { }

            }

            //app.Run(async context=>{
            //    var service = app.ApplicationServices.GetService<IAuthorizationService>();
            //    if (service != null)
            //    {

            //    }
            //});

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
