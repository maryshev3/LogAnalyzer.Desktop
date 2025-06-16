using LogParser.Models;

namespace LogAnalyzer.Desktop.ViewModels;

public class AnalyzeResultTabSwticherViewModel : ViewModelBase
{
    private readonly ParseAndAnalyzeResult _parseAndAnalyzeResult;

    public string AnalyzeContent => _parseAndAnalyzeResult.AnalyzeResult;
    
    public AnalyzeResultTabSwticherViewModel(ParseAndAnalyzeResult parseAndAnalyzeResult)
    {
        _parseAndAnalyzeResult = parseAndAnalyzeResult;
    }
}