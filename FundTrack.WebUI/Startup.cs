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
using FundTrack.WebUI.Formatter;
using Microsoft.AspNetCore.Http;
using FundTrack.WebUI.Middlewares;
using FundTrack.WebUI.Middlewares.Logging;

namespace FundTrack.WebUI
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
            // Available connection types : 'local','azure-main','azure-test', 'ss'
            string connectionType = "azure-main";
            services.AddDbContext<FundTrackContext>(options => options.UseSqlServer(Configuration.GetConnectionString(connectionType)));

            services.AddCors(
     options => options.AddPolicy("AllowCors",
         builder => {
             builder
                 //.WithOrigins("http://localhost:4456") //AllowSpecificOrigins;  
                 //.WithOrigins("http://localhost:4456", "http://localhost:4457") //AllowMultipleOrigins;  
                 .AllowAnyOrigin() //AllowAllOrigins;  
                                   //.WithMethods("GET") //AllowSpecificMethods;  
                                   //.WithMethods("GET", "PUT") //AllowSpecificMethods;  
                                   //.WithMethods("GET", "PUT", "POST") //AllowSpecificMethods;  
                 .WithMethods("GET", "PUT", "POST", "DELETE") //AllowSpecificMethods;  
                                                              //.AllowAnyMethod() //AllowAllMethods;  
                                                              //.WithHeaders("Accept", "Content-type", "Origin", "X-Custom-Header"); //AllowSpecificHeaders;  
                 .AllowAnyHeader(); //AllowAllHeaders;  
        })
 );


            // Add framework services.
            services.AddMvc(options =>
            {
                options.InputFormatters.Add(new JsonInputFormatter());
            });

            // for iis deploy : https://stackoverflow.com/questions/12731320/web-config-cannot-read-configuration-file-due-to-insufficient-permissions

            //dependency injection DAL
            services.AddTransient<IUserResporitory, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IOrganizationsForFilteringRepository, OrganizationsForFilteringRepository>();
            services.AddScoped<IEventManagementRepository, EventRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IRepository<Address>, AddressRepository>();
            services.AddScoped<IRepository<OrgAddress>, OrgAddressRepository>();
            services.AddScoped<IRepository<BankAccount>, BankAccountRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();
            services.AddScoped<IRepository<EventImage>, EventImageRepository>();
            services.AddScoped<IRequestedItemRepository, RequestedItemRepository>();
            services.AddScoped<IRepository<OfferedItem>, OfferedItemRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IGoodsCategoryRepository, GoodsCategoryRepository>();
            services.AddScoped<IUserResponseRepository, UserResponseRepository>();
            services.AddScoped<IRequestedItemImageRepository, RequestedItemImageRepository>();
            services.AddScoped<IGoodsTypeRepository, GoodsTypeRepository>();
            services.AddScoped<IOfferImagesRepository, OfferImagesRepository>();
            services.AddScoped<IBankImportDetailRepository, BankImportDetailRepository>();
            services.AddScoped<IOrganizationAccountRepository, OrganizationAccountRepository>();
            services.AddScoped<IRepository<Currency>, CurrencyRepository>();
            services.AddScoped<ITargetRepository, TargetRepository>();
            services.AddScoped<IDonationRepository, DonationRepository>();
            services.AddScoped<IFinOpRepository, FinOpRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IRepository<FinOpImage>, EFGenericRepository<FinOpImage>>();
            services.AddScoped<IBalanceRepository, BalanceRepository>();
            services.AddScoped<IBankRepository, BankRepositoty>();

            //dependency injection BLL
            services.AddScoped<IOrganizationsForFilteringService, OrganizationsForFilteringService>();
            services.AddTransient<IUserDomainService, UserDomainService>();
            services.AddScoped<IEventService, EventViewService>();
            services.AddScoped<IViewService<EventDetailViewModel>, EventDetailViewService>();
            services.AddScoped<IOrganizationRegistrationService, OrganizationRegistrationService>();
            services.AddScoped<ISuperAdminService, SuperAdminService>();
            services.AddScoped<IOrganizationProfileService, OrganizationProfileService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEventManagementService, EventManagementService>();
            services.AddScoped<IRequestedItemService, RequestedItemService>();
            services.AddScoped<IOfferedItemService, OfferedItemService>();
            services.AddScoped<IGoodsService, GoodsService>();
            services.AddScoped<IOrganizationProfileService, OrganizationProfileService>();
            services.AddScoped<IModeratorService, ModeratorService>();
            services.AddScoped<IUserResponseService, UserResponseService>();
            services.AddScoped<IBankImportService, BankImportService>();
            services.AddScoped<IOrganizationAccountService, OrganizationAccountService>();
            services.AddScoped<IDonateMoneyService, DonateMoneyService>();
            services.AddScoped<IFinOpService, FinOpService>();
            services.AddScoped<ITargetService, TargetService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IImageManagementService, AzureImageManagementService>();
            services.AddScoped<IFixingBalanceService, FixingBalanceService>();
            services.AddScoped<IBankService, BankService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseGlobalErrorHandling();

            //app.LoggingHandling();

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

            if (env.IsDevelopment())
            {
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            
            app.UseWebSockets();

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

            app.UseCors("AllowCors");
        }
    }
}
