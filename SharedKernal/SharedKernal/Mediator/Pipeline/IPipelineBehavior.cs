namespace SharedKernal.Mediator.Pipeline
{
    public interface IPipelineBehavior<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
    }

    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();
}