using ApiEmpleadosOAuth.Data;
using ApiEmpleadosOAuth.Helpers;
using ApiEmpleadosOAuth.Repositories;
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

namespace ApiEmpleadosOAuth
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
            services.AddTransient<HelperToken>();
            services.AddDbContext<EmpleadosContext>(options
                => options.UseSqlServer(cadena));
            //SWAGGER
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(name: "v1"
                        , new OpenApiInfo
                        {
                            Title = "Api Empleados Seguridad OAuth"
                            ,Version = "v1",
                            Description = "Ejemplo de seguridad OAuth Token"
                        });
                });

            HelperToken helper = new HelperToken(Configuration);
            //AÑADIMOS AUTHENTICATION CON LAS OPCIONES DEL HELPER
            services.AddAuthentication(helper.GetAuthOptions())
                .AddJwtBearer(helper.GetJwtBearerOptions());
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
                        url: "/swagger/v1/swagger.json"
                        , name: "Api v1");
                    c.RoutePrefix = "";
                });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
