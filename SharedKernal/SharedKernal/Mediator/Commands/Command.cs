using SharedKernal.Mediator.Interfaces;

namespace SharedKernal.Mediator.Commands
{
    public abstract record Command<TResponse> : ICommand<TResponse>
    {
        public DateTime Timestamp { get; } = DateTime.UtcNow;
    }

    public abstract record Command : Command<Unit>
    {
    }
}