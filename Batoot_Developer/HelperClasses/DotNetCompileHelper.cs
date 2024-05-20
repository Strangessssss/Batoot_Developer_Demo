using System.Diagnostics;

namespace Batoot_Developer.HelperClasses;

public static class DotNetCompileHelper
{
    
    public static void Build(string filePath)
    {
        CommandRun(filePath, "build");
    }
    
    public static void NewConsole(string filePath)
    {
        var psi = new ProcessStartInfo("dotnet")
        {
            Arguments = $"new console --force -o {filePath}"
        };
        Process.Start(psi);
    }

    public static void Run(string filePath)
    {
        CommandRun(filePath, $"run --project");
    }

    private static void CommandRun(string filePath, string arguments)
    {
        var path = $"{filePath.Remove(filePath.IndexOf(filePath.Split("\\").Last(), StringComparison.Ordinal))}";

        var process = new Process();

        var psi = new ProcessStartInfo("dotnet")
        {
            Arguments = $"{arguments} {path}"
        };
        process.StartInfo = psi;
        process.Start();
    }
}