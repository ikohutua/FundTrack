using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FundTrack.DAL.Abstract;
using FundTrack.BLL.Abstract;
using FundTrack.DAL.Concrete;
using FundTrack.BLL.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FundTrack.WebUI.token;
using FundTrack.DAL.Entities;
using FundTrack.BLL.DomainServices;
using FundTrack.DAL.Repositories;
using FundTrack.Infrastructure.ViewModel.EventViewModel;

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
            string connectionType = "ss";
            services.AddDbContext<FundTrackContext>(options => options.UseSqlServer(Configuration.GetConnectionString(connectionType)));

            // Add framework services.
            services.AddMvc();

            //services.AddSignalR();

            //dependency injection DAL
            services.AddTransient<IUserResporitory, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IOrganizationsForFilteringRepository, OrganizationsForFilteringRepository>();
            services.AddScoped<IRepository<Event>, EventRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IRepository<Address>, AddressRepository>();
            services.AddScoped<IRepository<OrgAddress>, OrgAddressRepository>();
            services.AddScoped<IRepository<BankAccount>, BankAccountRepository>();
            services.AddScoped<IRepository<OrgAccount>, OrgAccountRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();
            services.AddScoped<IRepository<EventImage>, EventImageRepository>();
            services.AddScoped<IRequestedItemRepository, RequestedItemRepository>();
            services.AddScoped<IRepository<OfferedItem>, OfferedItemRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IGoodsCategoryRepository, GoodsCategoryRepository>();
            services.AddScoped<IUserResponseRepository, UserResponseRepository>();

            //dependency injection BLL
            services.AddScoped<IOrganizationsForFilteringService, OrganizationsForFilteringService>();
            services.AddTransient<IUserDomainService, UserDomainService>();
            services.AddScoped<IEventService, EventViewService>();
            services.AddScoped<IViewService<EventDetailViewModel>, EventDetailViewService>();
            services.AddScoped<IOrganizationRegistrationService, OrganizationRegistrationService>();
            services.AddScoped<ISuperAdminService, SuperAdminService>();
            services.AddScoped<IOrganizationProfileService, OrganizationProfileService>();
            //services.AddScoped<IEditOrganizationService, EditOrganizationService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEventManagementService, EventManagementService>();
            services.AddScoped<IRequestedItemService, RequestedItemService>();
            services.AddScoped<IOfferedItemService, OfferedItemService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Bearer",
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                }
            });

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    LoginPath = new PathString("/User/LogIn"),
            //    AuthenticationScheme = "Bearer",
            //    AutomaticChallenge = true
            //});

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

            app.UseWebSockets();
            //app.UseSignalR();

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
