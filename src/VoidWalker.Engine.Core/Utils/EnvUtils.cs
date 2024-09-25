namespace VoidWalker.Engine.Core.Utils;

public static class EnvUtils
{
    public static bool IsDevelopment() => GetMode() == "dev";

    public static bool IsProduction() => GetMode() == "prod";

    public static string GetMode() => Environment.GetEnvironmentVariable("MODE") ?? "dev";
}
