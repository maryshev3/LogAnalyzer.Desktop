using System.Collections.Generic;

namespace LogAnalyzer.Desktop.Models.LogsTree;

public interface ILogsTreeNode
{
    public List<ILogsTreeNode> Children { get; }
    
    public string Content { get; }
}