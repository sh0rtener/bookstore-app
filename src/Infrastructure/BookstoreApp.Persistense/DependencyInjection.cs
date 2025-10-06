using System.Data;
using BookstoreApp.Domain.Books;
using BookstoreApp.Persistense.Dapper.Repositories;
using Npgsql;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddPersistanse(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        return services;
    }

    public static IServiceCollection AddNpgsqlConnection(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddScoped<IDbConnection>(
            (serviceProvider) => new NpgsqlConnection(connectionString)
        );
        return services;
    }
}
