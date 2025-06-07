using SharedKernal.Mediator.Interfaces;

namespace SharedKernal.Mediator
{
    public interface IMediator
    {
        Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
        Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
    }
}