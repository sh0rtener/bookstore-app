namespace BookstoreApp.Application.Pipelines;

public class PipelineBeforeResult
{
    public IQuery? Query { get; private set; }
    public ICommand? Command { get; private set; }

    public PipelineBeforeResult(IQuery? query = null, ICommand? command = null)
    {
        Query = query;
        Command = command;
    }
}

public interface IPipeline
{
    Task RunBeforeLogic(
        IQuery? query = null,
        ICommand? command = null,
        CancellationToken cancellationToken = default
    );
    Task Send(ICommand command, CancellationToken cancellationToken = default);
    Task<TResult> Send<TResult>(
        ICommand<TResult> command,
        CancellationToken cancellationToken = default
    );
    Task Send(IQuery query, CancellationToken cancellationToken = default);
    Task<TResult> Send<TResult>(
        IQuery<TResult> query,
        CancellationToken cancellationToken = default
    );
}
