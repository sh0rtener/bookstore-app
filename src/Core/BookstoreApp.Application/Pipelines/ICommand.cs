namespace BookstoreApp.Application.Pipelines;

public interface ICommand { }

public interface ICommand<in V> : ICommand { }

public interface ICommandHandlerBase { }

public interface ICommandHandler<in T> : ICommandHandlerBase
    where T : ICommand
{
    Task Handle(T command, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<in T, V> : ICommandHandlerBase
    where T : ICommand
{
    Task<V> Handle(T command, CancellationToken cancellationToken = default);
}
