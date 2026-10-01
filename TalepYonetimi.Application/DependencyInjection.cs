using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalepYonetimi.Application.Interfaces;
using TalepYonetimi.Application.Security;
using TalepYonetimi.Application.Services;
using TalepYonetimi.Domain.Interfaces;

namespace TalepYonetimi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(
        this IServiceCollection services)
        {

            services.AddScoped<ITalepService, TalepService>();
            services.AddScoped<IKullaniciService, KullaniciService>();
           services.AddScoped<ISifreHasher, Pbkdf2SifreHasher>();
            return services;
        }
    }
}
