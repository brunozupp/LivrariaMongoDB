using ElmahCore;
using ElmahCore.Mvc;
using Livraria.Api.Middlewares;
using Livraria.Domain.Handlers;
using Livraria.Domain.Interfaces.Respositories;
using Livraria.Infra.Data.DataContexts;
using Livraria.Infra.Data.Respositories;
using Livraria.Infra.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace LivrariaMongoDB.Api
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
            #region AppSettings

            AppSettings appSettings = new AppSettings();
            Configuration.GetSection("AppSettings").Bind(appSettings);
            services.AddSingleton(appSettings);

            #endregion AppSettings

            #region DataContexts

            services.AddScoped<LivroDataContext>();

            #endregion DataContexts

            services.AddAutoMapper(typeof(Startup));

            #region Respositories

            services.AddTransient<ILivroRepository, LivroRepository>();

            #endregion Respositories

            #region Handlers

            services.AddTransient<LivroHandler, LivroHandler>();

            #endregion Handlers

            #region Middlewares

            #endregion

            services.AddElmah();

            services.AddElmah<XmlFileErrorLog>(options =>
            {
                options.LogPath = "~/log";
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LivrariaMongoDB.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LivrariaMongoDB.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            // O acesso é feito pela URL: URL/elmah
            app.UseElmah();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
