using VoidWalker.Engine.Core.Interfaces.Services.Base;

namespace VoidWalker.Engine.Core.Interfaces.Services;

public interface IDataLoaderService : IVoidWalkerService
{
    void SubscribeToDataLoaded<T>(string type, Action<List<T>> callBack) where T : class;
}
