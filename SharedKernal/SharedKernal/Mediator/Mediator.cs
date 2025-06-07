using Microsoft.Extensions.DependencyInjection;
using SharedKernal.Mediator.Interfaces;
using SharedKernal.Mediator.Pipeline;

namespace SharedKernal.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
            var handler = _serviceProvider.GetRequiredService(handlerType);

            var behaviors = _serviceProvider.GetServices<IPipelineBehavior<ICommand<TResponse>, TResponse>>();
            var pipeline = behaviors.Reverse().Aggregate(
                (RequestHandlerDelegate<TResponse>)(() =>
                {
                    var method = handler.GetType().GetMethod("HandleAsync");
                    return (Task<TResponse>)method!.Invoke(handler, new object[] { command, cancellationToken })!;
                }),
                (next, pipeline) => () => pipeline.HandleAsync(command, next, cancellationToken));

            return await pipeline();
        }

        public async Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
            var handler = _serviceProvider.GetRequiredService(handlerType);

            var behaviors = _serviceProvider.GetServices<IPipelineBehavior<IQuery<TResponse>, TResponse>>();
            var pipeline = behaviors.Reverse().Aggregate(
                (RequestHandlerDelegate<TResponse>)(() =>
                {
                    var method = handler.GetType().GetMethod("HandleAsync");
                    return (Task<TResponse>)method!.Invoke(handler, new object[] { query, cancellationToken })!;
                }),
                (next, pipeline) => () => pipeline.HandleAsync(query, next, cancellationToken));

            return await pipeline();
        }
    }
}