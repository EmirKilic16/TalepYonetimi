using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TalepYonetimi.Application.Interfaces;
using TalepYonetimi.Domain.Interfaces;
using TalepYonetimi.Infrastructure.Data;
using TalepYonetimi.Infrastructure.Repositories;


namespace TalepYonetimi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IKullaniciRepository, KullaniciRepository>();
        services.AddScoped<ITalepRepository, TalepRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}
