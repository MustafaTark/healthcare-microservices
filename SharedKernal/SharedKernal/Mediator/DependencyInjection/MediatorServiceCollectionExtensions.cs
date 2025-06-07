using Microsoft.Extensions.DependencyInjection;
using SharedKernal.Mediator.Events;

namespace SharedKernal.Mediator.DependencyInjection
{
    public static class MediatorServiceCollectionExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<IEventPublisher, EventPublisher>();
            return services;
        }
    }
}