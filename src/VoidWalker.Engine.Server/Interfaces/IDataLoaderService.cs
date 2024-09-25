using VoidWalker.Engine.Core.Interfaces.Services;

namespace VoidWalker.Engine.Server.Interfaces;

public interface IDataLoaderService : IVoidWalkerService
{
    void SubscribeToDataLoaded<T>(string type, Action<List<T>> callBack) where T : class;
}
