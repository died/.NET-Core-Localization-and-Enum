using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EnumTest
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
            //set Support Cultures and default
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]{
                    new CultureInfo("en-US"),
                    new CultureInfo("zh-TW"),
                    new CultureInfo("ja-JP")
                };
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            //set resource path
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //seems don't need set it.
            //app.UseRequestLocalization(x=>
            //{
            //    x.SupportedCultures = _supportedCultures;
            //    x.SupportedUICultures = _supportedCultures;
            //    x.DefaultRequestCulture = new RequestCulture("en-US");
            //});

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
