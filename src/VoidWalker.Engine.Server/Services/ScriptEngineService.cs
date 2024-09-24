using Jint;
using VoidWalker.Engine.Core.Services.Base;
using VoidWalker.Engine.Server.Interfaces;
using VoidWalker.Engine.Server.Types;
using VoidWalker.Engine.Server.Utils;

namespace VoidWalker.Engine.Server.Services;

public class ScriptEngineService : BaseVoidWalkerService, IScriptEngineService
{
    private readonly Jint.Engine _engine;

    public ScriptEngineService(ILogger<ScriptEngineService> logger) : base(logger)
    {
        _engine = new Jint.Engine(
            options =>
            {
                options.StringCompilationAllowed = true;
                options.AllowClr();
            }
        );
    }

    public override async Task InitializeAsync()
    {
        var scriptsToLoad = DirectoriesUtils.GetFiles(DirectoryType.Scripts, "*.js").ToList();

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
}
