using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using LogAnalyzer.Desktop.Models.LogsTree;
using LogParser.Models;
using Avalonia.Controls.Models.TreeDataGrid;
using ReactiveUI;

namespace LogAnalyzer.Desktop.ViewModels;

public class AnalyzeResultTabSwticherViewModel : ViewModelBase
{
    private readonly ParseAndAnalyzeResult _parseAndAnalyzeResult;

    public string AnalyzeContent 
        => _parseAndAnalyzeResult.AnalyzeResult;

    private ObservableCollection<ILogsTreeNode> _errorsInFiles;
    public ObservableCollection<ILogsTreeNode> ErrorsInFiles
    {
        get => _errorsInFiles;
        set => this.RaiseAndSetIfChanged(ref _errorsInFiles, value);
    }

    private ILogsTreeNode _selectedItem;

    public ILogsTreeNode SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }
    
    public AnalyzeResultTabSwticherViewModel(ParseAndAnalyzeResult parseAndAnalyzeResult)
    {
        _parseAndAnalyzeResult = parseAndAnalyzeResult;

        ErrorsInFiles = TreefyLogItems(_parseAndAnalyzeResult.LogFileParseResult);
    }

    private ObservableCollection<ILogsTreeNode> TreefyLogItems(
        IEnumerable<LogFileParseResult> logFileParseResult)
    {
        ObservableCollection<ILogsTreeNode> result = new();

        foreach (var file in logFileParseResult)
        {
            result.Add(new LogFileNode(file));
        }

        return result;
    }
}