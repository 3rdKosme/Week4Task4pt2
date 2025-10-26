using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Week4Task4pt2.Application.Interfaces;
using Week4Task4pt2.Application.Services;
using Week4Task4pt2.Infrastructure.Persistence;
using Week4Task4pt2.Infrastructure.Persistence.Repositories;

namespace Week4Task4pt2.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LibraryContext>(options =>
        {
            options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
                );
        });

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthorService, AuthorService>();

        return services;
    }
}