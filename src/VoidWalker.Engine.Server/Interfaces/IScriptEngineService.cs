using VoidWalker.Engine.Core.Interfaces.Services;

namespace VoidWalker.Engine.Server.Interfaces;

public interface IScriptEngineService : IVoidWalkerService
{

    Task ExecuteFileAsync(string file);

}
