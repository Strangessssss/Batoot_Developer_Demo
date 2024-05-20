using System.IO;

namespace Batoot_Developer.HelperClasses;

public static class DirectoryValidator
{
    private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();

    public static bool IsValidDirectoryName(string directoryName)
    {
        if (string.IsNullOrWhiteSpace(directoryName))
            return false;

        if (directoryName.Length >= 16)
            return false;

        if (directoryName.Contains(Path.DirectorySeparatorChar.ToString()) ||
            directoryName.Contains(Path.AltDirectorySeparatorChar.ToString()))
            return false;

        if (directoryName.Intersect(InvalidFileNameChars).Any())
            return false;

        if (directoryName.Contains(" "))
            return false;
        
        return true;
    }
}