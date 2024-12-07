using System.Reflection;
using LCrm.Domain.Entries.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LCrm.Domain;

public static class DomainExtensions
{
    public static void RegisterDomain(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.RegisterHandlers();
    }
    
    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<CreateEntryCommand>, CreateEntryCommandHandler>();
        services.AddTransient<IRequestHandler<ChangeEntryStatusCommand>, ChangeEntryStatusCommandHandler>();
    }
}