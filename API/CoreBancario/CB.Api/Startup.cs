using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CB.Api.Route_transformer;
using CB.AplicationCore.Interfaces;
using CB.AplicationCore.Interfaces.Validations;
using CB.AplicationCore.Services;
using CB.AplicationCore.Validations;
using CB.Domain.Context;
using CB.Domain.Interfaces;
using CB.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace CB.Api
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
            services.AddControllers();

            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            });

            services.AddDbContext<CoreBancoDbContext>(confi =>
                confi.UseSqlServer(Configuration.GetConnectionString("CoreBancarioData")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core Bancario", Version = "v1" });
            });

            services.AddScoped<IMasterRepository, MasterRepository>();

            services.AddScoped<IGeneralValidationService, GeneralValidationService>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteValidationService, ClienteValidationService>();

            services.AddScoped<IBeneficiarioService, BeneficiarioService>();

            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IProductoValidationService, ProductoValidationService>();

            services.AddScoped<ICuentaAhorroService, CuentaAhorroService>();
            services.AddScoped<ICuentaAhorroValidationService, CuentaAhorroValidationService>();

            services.AddScoped<ITarjetaCreditoService, TarjetaCreditoService>();
            services.AddScoped<ITarjetaCreditoValidationService, TarjetaCreditoValidationService>();

            services.AddScoped<ITransaccionService, TransaccionService>();
            services.AddScoped<ITransaccionValidationService, TransaccionValidationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core Bancario V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
