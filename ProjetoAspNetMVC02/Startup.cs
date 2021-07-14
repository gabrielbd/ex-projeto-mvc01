using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoAspNetMVC02.Interfaces;
using ProjetoAspNetMVC02.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC02
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
            //Adicionando uma configuração para habilitar
            //no projeto o uso de CONTROLLERS e VIEWS
            services.AddControllersWithViews();

            //ler a connectionstring que está mapeada no arquivo appsettings.json
            var connectionstring = Configuration.GetConnectionString("Conexao");

            //passar a connectionstring para a classe ClienteRepository
            services.AddTransient<IClienteRepository, ClienteRepository>
                (map => new ClienteRepository(connectionstring));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //definindo a página que será exibida no navegador
            //quando o projeto for acessado ou executado
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default", pattern : "{controller=Home}/{action=Index}");
            });
        }
    }
}
