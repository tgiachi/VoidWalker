using VoidWalker.Engine.Core.Extensions;
using VoidWalker.Engine.Server.Types;

namespace VoidWalker.Engine.Server.Utils;

public static class DirectoriesUtils
{
    private const string homeDirectory = "voidwalker";

    public static string GetRootDirectory()
    {
        var rootDirectory = Environment.GetEnvironmentVariable("VW_ROOT_DIR");


        if (string.IsNullOrEmpty(rootDirectory))
        {
            rootDirectory = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                homeDirectory
            );
        }

        if (rootDirectory == homeDirectory && Environment.GetEnvironmentVariable("IS_IN_CONTAINER") == "true")
        {
            rootDirectory = Path.Join(Directory.GetCurrentDirectory(), homeDirectory);
        }


        if (!Directory.Exists(rootDirectory))
        {
            Directory.CreateDirectory(rootDirectory);
        }

        Environment.SetEnvironmentVariable("VW_ROOT_DIR", rootDirectory);
        Environment.SetEnvironmentVariable("PATH_SEPARATOR", Path.DirectorySeparatorChar.ToString());

        CheckAndCreateDirectories(rootDirectory);

        return rootDirectory;
    }

    public static void CheckAndCreateDirectories(string rootDirectory)
    {
        foreach (var d in Enum.GetNames<DirectoryType>())
        {
            var dir = Path.Join(rootDirectory, d.ToSnakeCase());
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }

    public static string GetDirectoryPath(DirectoryType directoryType) =>
        Path.Join(GetRootDirectory(), directoryType.ToString().ToLower());


    public static IEnumerable<string> GetFiles(DirectoryType directoryType, string searchPattern = "*") =>
        Directory.GetFiles(GetDirectoryPath(directoryType), searchPattern);
}
