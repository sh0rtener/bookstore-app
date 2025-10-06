namespace BookstoreApp.Application.Pipelines;

public interface IPipelineBefore
{
    Task Handle<T>(T request, CancellationToken cancellationToken = default);
}
