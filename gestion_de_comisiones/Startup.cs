using gestion_de_comisiones.GuardianModels;
using gestion_de_comisiones.MultinivelModel;
using gestion_de_comisiones.Repository;
using gestion_de_comisiones.Repository.Interfaces;
using gestion_de_comisiones.Servicios;
using gestion_de_comisiones.Servicios.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Text;

namespace gestion_de_comisiones
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
            //agregar autenticacion por tocker
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
           { 
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
            });

            //services.AddAutoMapper(typeof(MappingProfiles));

            services.AddControllersWithViews();

            //interfaces de servicios
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IFacturaService, FacturaService>();
            services.AddScoped<IAplicacionesService, AplicacionesService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IProrrateadoService, ProrrateadoService>();
            services.AddScoped<IFormaPagoService, FormaPagoService>();
            services.AddScoped<IGestionPagosService, GestionPagosService>();
            services.AddScoped<IGestionPagosRezagadosService, GestionPagosRezagadosService>();
            services.AddScoped<IReporteService, ReporteService>();

            //interfaces de repositorios
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IClienteRepository, ClienteRespository>();
            services.AddScoped<IPaisRepository, PaisRepository>();
            services.AddScoped<IFacturaRepository, FacturaRepository>();
            services.AddScoped<IAplicacionesRepository, AplicacionesRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProrrateadoRepository, ProrrateadoRepository>();
            services.AddScoped<IFormaPagoRepository, FormaPagoRespository>();
            services.AddScoped<IGestionPagoRepository, GestionPagoRepository>();
            services.AddScoped<IGestionPagosRezagadosRepository, GestionPagosRezagadosRepository>();
            services.AddScoped<IReporteRepository, ReporteRepository>();

            services.AddScoped<BDMultinivelContext>();
            services.AddScoped<grdsionContext>();


            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            //configuracion y conexion al db context


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //var path = Directory.GetCurrentDirectory();
           // loggerFactory.AddFile($"{path}\\Logs\\Log-gestor.txt");
            loggerFactory.AddFile("./Logs/Log-gestor-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization(); // descomentar para que funcione el leer el token

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.Options.StartupTimeout = System.TimeSpan.FromSeconds(180);
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
