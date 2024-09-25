using System.Reflection;
using Jint;
using VoidWalker.Engine.Core.Attributes.Scripts;
using VoidWalker.Engine.Core.Data.Scripts;
using VoidWalker.Engine.Core.Services.Base;
using VoidWalker.Engine.Server.Interfaces;
using VoidWalker.Engine.Server.Types;
using VoidWalker.Engine.Server.Utils;
using Expression = System.Linq.Expressions.Expression;

namespace VoidWalker.Engine.Server.Services;

public class ScriptEngineService : BaseVoidWalkerService, IScriptEngineService
{
    private readonly Jint.Engine _engine;
    private readonly Dictionary<string, object> _scriptConstants = new();

    private readonly string _fileExtension = "*.js";

    public List<ScriptFunctionDescriptor> Functions { get; } = new();

    public Dictionary<string, object> ContextVariables { get; } = new();
    public Task<string> GenerateTypeDefinitionsAsync() => throw new NotImplementedException();

    public ScriptEngineService(ILogger<ScriptEngineService> logger) : base(logger)
    {
        _engine = new Jint.Engine(
            options =>
            {
                options.DebugMode(true);
                //options.TimeoutInterval(TimeSpan.FromSeconds(4));
                // Limit the memory to 4Gb
                options.LimitMemory(4_000_000_000);


                options.EnableModules(DirectoriesUtils.GetDirectoryPath(DirectoryType.Modules));
                options.StringCompilationAllowed = true;
            }
        );
    }

    public override async Task InitializeAsync()
    {
        var scriptsToLoad = DirectoriesUtils.GetFiles(DirectoryType.Scripts, _fileExtension).ToList();

        foreach (var script in scriptsToLoad)
        {
            ExecuteFileAsync(script);
        }
    }

    public async Task ExecuteFileAsync(string file)
    {
        Logger.LogInformation("Executing script: {File}", Path.GetFileName(file));
        try
        {
            var script = await File.ReadAllTextAsync(file);
            _engine.Execute(script);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error executing script: {File}", Path.GetFileName(file));
        }
    }

    private void ExtractFunctionDescriptor(ScriptFunctionAttribute attribute, MethodInfo methodInfo)
    {
        var descriptor = new ScriptFunctionDescriptor
        {
            FunctionName = attribute.Alias ?? methodInfo.Name,
            Help = attribute.Help,
            Parameters = new List<ScriptFunctionParameterDescriptor>(),
            ReturnType = methodInfo.ReturnType.Name,
            RawReturnType = methodInfo.ReturnType
        };

        foreach (var parameter in methodInfo.GetParameters())
        {
            descriptor.Parameters.Add(
                new ScriptFunctionParameterDescriptor(parameter.Name, parameter.ParameterType.Name, parameter.ParameterType)
            );
        }

        Functions.Add(descriptor);
    }

    public ScriptEngineExecutionResult ExecuteCommand(string command)
    {
        try
        {
            var result = new ScriptEngineExecutionResult { Result = _engine.Evaluate(command) };

            return result;
        }
        catch (Exception ex)
        {
            return new ScriptEngineExecutionResult() { Exception = ex };
        }
    }


    private void AddContextVariable(string name, object value)
    {
        _engine.SetValue(name, value);
        ContextVariables[name] = value;
    }

    private Delegate CreateJsEngineDelegate(object obj, MethodInfo method)
    {
        return method.CreateDelegate(
            Expression.GetDelegateType(
                (from parameter in method.GetParameters() select parameter.ParameterType)
                .Concat(new[] { method.ReturnType })
                .ToArray()
            ),
            obj
        );
    }
}
