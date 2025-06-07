namespace SharedKernal.Mediator.Interfaces
{
    public interface ICommand<TResponse>
    {
        DateTime Timestamp { get; }
    }

    public interface ICommand : ICommand<Unit>
    {
    }
}