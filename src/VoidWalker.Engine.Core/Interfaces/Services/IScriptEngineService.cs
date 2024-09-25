using VoidWalker.Engine.Core.Data.Scripts;
using VoidWalker.Engine.Core.Interfaces.Services.Base;

namespace VoidWalker.Engine.Core.Interfaces.Services;

public interface IScriptEngineService : IVoidWalkerService
{
    Task ExecuteFileAsync(string file);

    ScriptEngineExecutionResult ExecuteCommand(string command);

    List<ScriptFunctionDescriptor> Functions { get; }

    Dictionary<string, object> ContextVariables { get; }

    Task<string> GenerateTypeDefinitionsAsync();
}
