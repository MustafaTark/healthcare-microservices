using Microsoft.Extensions.DependencyInjection;

namespace SharedKernal.Mediator.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : IEvent
        {
            var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
            var tasks = handlers.Select(h => h.HandleAsync(@event, cancellationToken));
            await Task.WhenAll(tasks);
        }
    }
}