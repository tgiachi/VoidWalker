using VoidWalker.Engine.Core.Attributes.Scripts;

namespace VoidWalker.Engine.Server.ScriptModules;

[ScriptModule]
public class TestModule
{
    [ScriptFunction("Add", "Adds two numbers together.")]
    public int Add(int a, int b) => a + b;
}
