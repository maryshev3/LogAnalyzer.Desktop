using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform.Storage;

namespace LogAnalyzer.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.KeyDown += OnKeyDown;
    }
    
    private bool IsCalledPasteCommand(KeyEventArgs e) 
        => e.Key == Key.V &&
        (
            (e.KeyModifiers & KeyModifiers.Control) == KeyModifiers.Control
            || (e.KeyModifiers & KeyModifiers.Meta) == KeyModifiers.Meta
        );
    
    private async void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (IsCalledPasteCommand(e))
        {
            var clipboard = TopLevel.GetTopLevel(this).Clipboard;
            
            var data = await clipboard.GetDataAsync(DataFormats.Files);

            if (data != null && data is IEnumerable<IStorageItem> files)
            {
                ;
            }
        }
    }

}