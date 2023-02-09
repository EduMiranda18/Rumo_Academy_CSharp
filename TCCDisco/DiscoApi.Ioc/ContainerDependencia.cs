using DiscoApi.Respositorios.Repositorio;
using DiscoApi.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscoApi.Ioc
{
    public class ContainerDependencia
    {

        public static void RegistrarServicos(IServiceCollection services)
        {
            //repositorios
            services.AddScoped<DiscoRepositorio, DiscoRepositorio>();

            //services
            services.AddScoped<DiscoService, DiscoService>();
        }

    }
}
