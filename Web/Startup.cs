using Domain;
using Domain.Data;
using Domain.Repositories;
using Domain.Security;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Text;

namespace Web
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
            services.AddDataProtection()
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });

            services.AddDbContext<HtmlToWordDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Default"));
            });

            services.AddSingleton<IHtmlParser, HtmlParser>();
            services.AddSingleton<IEncryptionKeyProvider, ConfigurationFileKeyProvider>();
            services.AddSingleton<HttpClient>();

            services.AddScoped<IEncryptionProvider>(provider =>
            {
                var keyProvider = provider.GetRequiredService<IEncryptionKeyProvider>();
                return new RSAEncryptionProvider(RSAType.RSA2, Encoding.UTF8, keyProvider);
            });
            services.AddScoped<IHashProvider, HashingProvider>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IWordDictionaryRepository, WordDictionaryRepository>();
            services.AddScoped<IWordDictionaryService, WordDictionaryService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                 options =>
                 {
                     options.SlidingExpiration = true;
                     options.LoginPath = new PathString("/Account/login");
                     options.AccessDeniedPath = new PathString("/Account/denied");
                     options.ClaimsIssuer = "HtmlToWord.com";
                     options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                 });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

}
