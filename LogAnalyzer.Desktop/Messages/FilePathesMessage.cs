using System;
using System.Collections.Generic;

namespace LogAnalyzer.Desktop.Messages;

public class FilePathesMessage
{
    public IEnumerable<string> FullPathes { get; set; } = Array.Empty<string>();
}