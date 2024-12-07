using System.Data;
using LCrm.Domain;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace LCrm.EventStore;

public static class EventStoreExtensions
{
    public static void RegisterEventStore(this IServiceCollection services)
    {
        services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection("Host=localhost; Database=lcrm; Username=admin; Password=password;"));
        services.AddScoped<IEventStore, EventStore>();
    }
}