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
            //for DBContext
            //string connection = Configuration.GetConnectionString("FundTrackDBConnection");
            //services.AddDbContext<FundTrackContext>(options => options.UseSqlServer(connection));

            // Add framework services.
            services.AddMvc();

            //dependency injection DAL
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserDomainService, UserDomainService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrganizationsListRepository, OrganizationsListRepository>();

            //dependency injection BLL
            services.AddScoped<IOrganizationsForLayoutService, OrganizationsForLayoutService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithReExecute("/Error/Index", "?statusCode={0}");

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
            }
            else
            {
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                //Commented, because it has an impact on error handling. If app continues to work fine for next 2-3 weeks - this will be removed.
                //routes.MapSpaFallbackRoute(
                //    name: "spa-fallback",
                //    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
