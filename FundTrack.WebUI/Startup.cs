using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Repositories;
using FundTrack.BLL.Abstract;
using FundTrack.BLL.DomainServices;
using FundTrack.DAL.Concrete;
using FundTrack.BLL.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FundTrack.WebUI.token;
using System.Text;
using FundTrack.DAL.Entities;

namespace FundTrack_WebUI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Db Connection
            // For local connection, go to appsettings.json and write your local connection string
            // Available connection types : 'local','azure-main','azure-test'
            string connectionType = "local";
            services.AddDbContext<FundTrackContext>(options => options.UseSqlServer(Configuration.GetConnectionString(connectionType)));

            // Add framework services.
            services.AddMvc();

            //dependency injection DAL
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrganizationsListRepository, OrganizationsListRepository>();

            //dependency injection BLL
            services.AddScoped<IOrganizationsForLayoutService, OrganizationsForLayoutService>();
            services.AddTransient<IUserDomainService, UserDomainService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme="Bearer",
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                }
            });
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithReExecute("/Error/Index", "?statusCode={0}");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                //Have to uncomment this, because page refreshing throws 404 in SPA
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
