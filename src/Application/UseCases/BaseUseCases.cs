namespace AIGenManager.Application.UseCases;

public abstract class UseCase<TRequest, TResponse>
{
    public abstract Task<TResponse> ExecuteAsync(TRequest request);
}

public abstract class UseCase<TResponse>
{
    public abstract Task<TResponse> ExecuteAsync();
}
