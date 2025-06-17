using System.Collections.Generic;
using LogParser.Models;

namespace LogAnalyzer.Desktop.Models.LogsTree;

public class LogItemNode : ILogsTreeNode
{
    public LogItem LogItem { get; }

    public LogItemNode(LogItem item)
    {
        LogItem = item;
    }

    public List<ILogsTreeNode> Children { get; }

    public string Content => LogItem.LogMetaData.DateTimeOffset.ToString();

    public override string ToString()
    {
        return LogItem.LogContent;
    }
}