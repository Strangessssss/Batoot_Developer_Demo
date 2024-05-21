using System.Diagnostics;

namespace Batoot_Developer.HelperClasses;

public static class DotNetCompileHelper
{
    
    public static void Build(string filePath, out string errors)
    {
        CommandRun(filePath, "build", out errors, true);
    }
    
    public static void NewConsole(string filePath)
    {
        var psi = new ProcessStartInfo("dotnet")
        {
            Arguments = $"new console --force -o {filePath}",
            CreateNoWindow = true
        };
        Process.Start(psi);
        
    }

    public static void Run(string filePath, out string errors)
    {
        CommandRun(filePath, $"run --project", out errors, false);
    }
    
    private static void CommandRun(string filePath, string arguments, out string errors, bool shell)
    {
        var path = $"{filePath.Remove(filePath.IndexOf(filePath.Split("\\").Last(), StringComparison.Ordinal))}";
        
        var process = new Process();
        
        var psi = new ProcessStartInfo("dotnet")
        {
            Arguments = $"{arguments} {path}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = shell
        };
        process.StartInfo = psi;
        process.Start();
        errors = $"{process.StandardOutput.ReadToEnd()}";
    }
}