using ApiEmpleadosLogicApps.Data;
using ApiEmpleadosLogicApps.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEmpleadosLogicApps
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            String cadena =
                this.Configuration.GetConnectionString("cadenahospital");
            services.AddTransient<RepositoryEmpleados>();
            services.AddDbContext<EmpleadosContext>(options
                => options.UseSqlServer(cadena));
            //SWAGGER
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(name: "v2"
                        , new OpenApiInfo
                        {
                            Title = "Api Tareas Logic App"
                            ,Version = "2.0",
                            Description = "Ejemplo de Logic Apps Api"
                        });
                });
            services.AddControllers();
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            //UI INDICA DONDE VA A VISUALIZAR EL USUARIO LA DOCUMENTACION
            //GENERADA POR SWAGGER EN NUESTRO SERVIDOR
            app.UseSwaggerUI(
                c =>
                {
                    //DEBEMOS CONFIGURAR LA URL DEL SERVIDOR
                    //PARA LA DOCUMENTACION
                    c.SwaggerEndpoint(
                        url: "/swagger/v2/swagger.json"
                        , name: "Api v2");
                    c.RoutePrefix = "";
                });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
