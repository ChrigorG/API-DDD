using Application.Application;
using Application.Interface;
using Domain.Interface;
using Domain.Interface.InterfaceService;
using Domain.Service;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureIoC(this IServiceCollection services)
        {
            // Interface e Repositorios
            services.AddScoped<INews, NewsRepository>();
            services.AddScoped<IUser, UserRepository>();

            // Interface e Serviço
            services.AddScoped<INewsService, NewsService>();

            // Interface Aplicação
            services.AddScoped<INewsApplication, NewsApplication>();
            services.AddScoped<IUserApplication, UserApplication>();

            return services;
        }
    }
}
