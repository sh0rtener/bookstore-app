namespace BookstoreApp.Application.Pipelines;

public class Pipeline : IPipeline
{
    private readonly List<IPipelineBefore>? _pipelineBefores = [];
    private readonly Dictionary<Type, ICommandHandler<ICommand>>? _commands;
    private readonly Dictionary<Type, IQueryHandlerBase>? _queries;

    public Pipeline(
        List<IPipelineBefore>? pipelineBefores = null,
        Dictionary<Type, ICommandHandler<ICommand>>? commands = null,
        Dictionary<Type, IQueryHandlerBase>? queries = null
    )
    {
        _pipelineBefores = pipelineBefores;
        _commands = commands;
        _queries = queries;
    }

    public async Task RunBeforeLogic(
        IQuery? query = null,
        ICommand? command = null,
        CancellationToken cancellationToken = default
    )
    {
        if (_pipelineBefores is null)
            return;

        foreach (var pipelineBefore in _pipelineBefores)
        {
            if (query is null && command is null)
                throw new InvalidDataException(
                    "Ошибка с пайплайнами. Команды или запросы не переданы."
                );

            if (query is not null)
                await pipelineBefore.Handle(query, cancellationToken);
            else
                await pipelineBefore.Handle(command, cancellationToken);
        }
    }

    public async Task Send(ICommand command, CancellationToken cancellationToken = default)
    {
        await RunBeforeLogic(command: command, cancellationToken: cancellationToken);
        if (_commands is null)
            throw new InvalidDataException(
                "Ошибка с пайплайнами. Ни одна команда не зарегистрирована"
            );
        if (!_commands.ContainsKey(command.GetType()))
            throw new InvalidCastException("Ошибка с пайплайнами. Команда не найдена");

        await _commands[command.GetType()].Handle(command, cancellationToken);
    }

    public async Task<TResult> Send<TResult>(
        ICommand<TResult> command,
        CancellationToken cancellationToken = default
    )
    {
        await RunBeforeLogic(command: command, cancellationToken: cancellationToken);
        if (_commands is null)
            throw new InvalidDataException(
                "Ошибка с пайплайнами. Ни одна команда с результатом не зарегистрирована"
            );
        if (!_commands.ContainsKey(command.GetType()))
            throw new InvalidCastException("Ошибка с пайплайнами. Команда не найдена");

        dynamic handler = _commands[command.GetType()];
        return await handler.Handle((dynamic)command, cancellationToken);
    }

    public async Task Send(IQuery query, CancellationToken cancellationToken = default)
    {
        await RunBeforeLogic(query: query, cancellationToken: cancellationToken);
        if (_queries is null)
            throw new InvalidDataException(
                "Ошибка с пайплайнами. Ни один запрос не зарегистрирован"
            );
        if (!_queries.ContainsKey(query.GetType()))
            throw new InvalidCastException("Ошибка с пайплайнами. Запрос не найден");

        await ((IQueryHandler<IQuery>)_queries[query.GetType()]).Handle(query, cancellationToken);
    }

    public async Task<TResult> Send<TResult>(
        IQuery<TResult> query,
        CancellationToken cancellationToken = default
    )
    {
        await RunBeforeLogic(query: query, cancellationToken: cancellationToken);
        if (_queries is null)
            throw new InvalidDataException(
                "Ошибка с пайплайнами. Ни один запрос не зарегистрирован"
            );
        if (!_queries.ContainsKey(query.GetType()))
            throw new InvalidCastException("Ошибка с пайплайнами. Запрос не найден");
        dynamic handler = _queries[query.GetType()];
        return await handler.Handle((dynamic)query, cancellationToken);
    }
}
