using Microsoft.Extensions.DependencyInjection;
using VoidWalker.Engine.Core.Extensions;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Server.Services;

namespace VoidWalker.Engine.Tests;

public class GameServerTests
{
    private IServiceCollection _serviceCollection;
    private ServiceProvider _serviceProvider;

    [SetUp]
    public void Setup()
    {
        _serviceCollection = new ServiceCollection();
        
        _serviceCollection.RegisterVoidWalkerService<ISessionService, SessionService>()
            .RegisterVoidWalkerService<ITileSetService, TileSetService>()
            .RegisterVoidWalkerService<IMessageBusService, MessageBusService>()
            .RegisterVoidWalkerService<INetworkDispatcherService, NetworkDispatcherService>()
            .RegisterVoidWalkerService<IScriptEngineService, ScriptEngineService>(true, 101)
            .RegisterVoidWalkerService<IDataLoaderService, DataLoaderService>(true, 100);


        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }


    [TearDown]
    public void TearDown()
    {
        _serviceProvider.Dispose();
    }
}
