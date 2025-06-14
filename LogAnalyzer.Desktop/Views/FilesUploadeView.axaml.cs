using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace LogAnalyzer.Desktop.Views;

public partial class FilesUploadeView : UserControl
{
    private readonly string _dragAndDropElementName = "DragAndDropElement";
    private readonly string _overedDragAndDropBorderBrushName = "OveredDragAndDropBorderBrush";

    private IBrush? _originDragAndDropBorderBrush;
    private IBrush _overedDragAndDropBorderBrush;
    
    public FilesUploadeView()
    {
        InitializeComponent();

        InitializeDragAndDrop();
    }

    private void InitializeDragAndDrop()
    {
        var dragAndDrop = this.Find<Border>(_dragAndDropElementName);

        _originDragAndDropBorderBrush = dragAndDrop.BorderBrush;

        if (Application.Current.TryFindResource(
            _overedDragAndDropBorderBrushName,
            out var overedDragAndDropBorderBrush
        ))
        {
            _overedDragAndDropBorderBrush = (SolidColorBrush)overedDragAndDropBorderBrush;
        };
        
        dragAndDrop.AddHandler(DragDrop.DragOverEvent, OnDragOverBorder);
        dragAndDrop.AddHandler(DragDrop.DragLeaveEvent, OnDragLeaveBorder);
        dragAndDrop.AddHandler(DragDrop.DropEvent, OnDropBorder);
    }
    
    private void OnDragOverBorder(object? sender, DragEventArgs e)
    {
        if (e.Data.Contains(DataFormats.FileNames))
        {
            // Изменяем цвет границы при наведении.
            ((Border)sender).BorderBrush = _overedDragAndDropBorderBrush;
            
            e.DragEffects = DragDropEffects.Copy;
        }
        else
        {
            e.DragEffects = DragDropEffects.None;
        }
    }

    private void OnDragLeaveBorder(object? sender, DragEventArgs e)
    {
        // Возвращаем исходную заливку Border при уходе курсора
        ((Border)sender).BorderBrush = _originDragAndDropBorderBrush;
    }

    private void OnDropBorder(object? sender, DragEventArgs e)
    {
        // После дропа возвращаем исходную заливку и обрабатываем файлы
        ((Border)sender).BorderBrush = _originDragAndDropBorderBrush;
        
        if (e.Data.Contains(DataFormats.FileNames))
        {
            var files = e.Data.GetFileNames();
            
            // Обработка файлов.
        }
    }

}