namespace BookstoreApp.Application.Pipelines;

public interface IQuery { }

public interface IQuery<in V> : IQuery { }

public interface IQueryHandlerBase { }

public interface IQueryHandler<in T> : IQueryHandlerBase
    where T : IQuery
{
    Task Handle(T query, CancellationToken cancellationToken = default);
}

public interface IQueryHandler<in T, V> : IQueryHandlerBase
    where T : IQuery
{
    Task<V> Handle(T query, CancellationToken cancellationToken = default);
}
