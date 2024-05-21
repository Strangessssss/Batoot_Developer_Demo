using System.Collections.ObjectModel;
using Microsoft.CodeAnalysis.CSharp;

namespace Batoot_Developer.ErrorDetector;

public static class CodeAnalyzer
{
    public static ObservableCollection<ErrorInfo> AnalyzeErrors(string code)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        CSharpCompilation.Create("CodeAnalysis", new[] { syntaxTree });

        var diagnostics = syntaxTree.GetDiagnostics();

        var errorList = new ObservableCollection<ErrorInfo>();

        foreach (var diagnostic in diagnostics)
        {
            var lineSpan = diagnostic.Location.GetLineSpan().Span;
            errorList.Add(new ErrorInfo
            {
                LineNumber = lineSpan.Start.Line + 1,
                ErrorMessage = diagnostic.GetMessage(),
                LogLevel = diagnostic.Severity
            });
        }

        return errorList;
    }
}