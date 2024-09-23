namespace VoidWalker.Engine.Core.Exceptions;

public class ConfigSectionNotFoundException : Exception
{
    public ConfigSectionNotFoundException(string sectionName) : base($"Configuration section '{sectionName}' not found.")
    {
    }
}
