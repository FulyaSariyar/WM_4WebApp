using ItServiceApp.Business.MappersProfiles;
using ItServiceApp.Business.Services.Email;
using ItServiceApp.Business.Services.Payment;
using ItServiceApp.Core.Identity;
using ItServiceApp.Data;
using ItServiceApp.Extensions;
using ItServiceApp.InjectOrnek;
using ItServiceApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;

namespace ItServiceApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration= configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));

            });
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 5;

                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromMinutes(1);
                    options.Lockout.AllowedForNewUsers = false;

                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters = "";


                    //options.SignIn.RequireConfirmedEmail = true;
                    
                }).AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                //Cookie settings
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;

            });

            services.AddAutoMapper(options =>
            {
                options.AddProfile(typeof(AccountProfile));
                options.AddProfile(typeof(PaymentProfile));
                options.AddProfile<SubscriptionProfiles>();
            });
 
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IMyDependency, NewMyDependency>(); //Dikkat!!!!! loose coupling 
            services.AddScoped<IPaymentService, IyzicoPaymentService>();
            services.AddAppRepositories();


            services.AddControllersWithViews()
                .AddNewtonsoftJson(
                options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
                

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),
                @"node_modules")),
                RequestPath = new PathString("/vendor")
            });

           
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute("admin","admin","admin/{controller=Manage}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
