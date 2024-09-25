using Microsoft.Extensions.Logging;
using VoidWalker.Engine.Core.Interfaces.Services;

namespace VoidWalker.Engine.Core.Services.Base;

public abstract class BaseVoidWalkerService : IVoidWalkerService
{
    protected ILogger Logger { get; }

    protected BaseVoidWalkerService(ILogger logger)
    {
        Logger = logger;
    }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }



}
