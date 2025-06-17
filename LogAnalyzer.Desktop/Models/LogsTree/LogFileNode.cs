using System.Collections.Generic;
using System.Linq;
using LogParser.Models;

namespace LogAnalyzer.Desktop.Models.LogsTree;

public class LogFileNode : ILogsTreeNode
{
    public string SourceFileName { get; }
    public List<ILogsTreeNode> Children { get; set; }

    public LogFileNode(LogFileParseResult result)
    {
        SourceFileName = result.SourceFileName;
        Children = result
            .LogItems
            .Select(item => new LogItemNode(item))
            .Cast<ILogsTreeNode>()
            .ToList();
    }

    public string Content => $"({Children.Count}) {SourceFileName}";

    public override string ToString()
    {
        return "Для просмотра содержимого выберите конкретную ошибку";
    }
}