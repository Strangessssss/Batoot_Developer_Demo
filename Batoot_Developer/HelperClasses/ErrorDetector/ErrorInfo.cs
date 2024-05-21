using Microsoft.CodeAnalysis;

namespace Batoot_Developer.ErrorDetector;

public class ErrorInfo
{
    public int LineNumber { get; set; }
    public string? ErrorMessage { get; set; }
    
    public DiagnosticSeverity? LogLevel { get; set; }
}