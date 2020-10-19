using INTELECTAH.ConsultaFacil.CommonRespository;
using INTELECTAH.ConsultaFacil.Repository.SqlServer.Implementations;
using INTELECTAH.ConsultaFacil.Service;
using INTELECTAH.ConsultaFacil.Service.Implementations;
using INTELECTAH.ConsultaFacil.Service.Interfaces;
using INTELECTAH.ConsultaFacil.SQLServerRepository;
using INTELECTAH.ConsultaFacil.WebApi.Configurations;
using INTELECTAH.ConsultaFacil.WebApi.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace INTELECTAH.ConsultaFacil.WebApi
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
            // Context
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Config
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));

            // Dependency injection
            services.AddScoped<IPacienteRepository, PacienteSQLServerRespository>();
            services.AddScoped<IPacienteService, PacienteService>();

            services.AddScoped<IConsultaRepository, ConsultaSQLServerRespository>();
            services.AddScoped<IConsultaService, ConsultaService>();

            services.AddScoped<IExameRepository, ExameSQLServerRepository>();
            services.AddScoped<IExameService, ExameService>();

            services.AddScoped<ITipoExameRepository, TipoExameSQLServerRepository>();
            services.AddScoped<ITipoExameService, TipoExameService>();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Consulta Fácil API",
                    Version = "v1",
                    Description = "Um serviço de API para o Consulta Fácil",
                    Contact = new OpenApiContact
                    {
                        Name = "Leandro Andrade",
                        Email = "eusouleoandrade@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/eusouleoandrade/")
                    }
                });
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Consulta Fácil API V1");
                c.RoutePrefix = string.Empty;
                c.DefaultModelsExpandDepth(-1);
            });
        }
    }
}
