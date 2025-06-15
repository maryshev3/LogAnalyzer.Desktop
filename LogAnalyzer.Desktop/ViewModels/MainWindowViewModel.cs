using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using LogAnalyzer.Desktop.Messages;
using LogAnalyzer.Services;
using LogParser.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace LogAnalyzer.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly LogAnalyzerClient _logAnalyzerClient;

    private bool _isAnalyzeActive;
    public bool IsAnalyzeActive
    {
        get => _isAnalyzeActive;
        set => this.RaiseAndSetIfChanged(ref _isAnalyzeActive, value);
    }
    
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentViewModel, value);
            this.RaisePropertyChanged(nameof(IsToLoadButtonActive));
        }
    }

    public bool IsToLoadButtonActive =>
        CurrentViewModel is not FilesUploadeViewModel;

    public void ToLoadView()
    {
        CurrentViewModel = Program.GetRequiredService<FilesUploadeViewModel>();
    }
    
    public MainWindowViewModel(LogAnalyzerClient logAnalyzerClient)
    {
        _logAnalyzerClient = logAnalyzerClient;

        IsAnalyzeActive = false;
        
        MessageBus
            .Current
            .Listen<FilePathesMessage>()
            .Subscribe(Observer.Create<FilePathesMessage>(async message =>
            {
                if (IsAnalyzeActive)
                {
                    return;
                }

                IsAnalyzeActive = true;
                
                if (!message.FullPathes.Any())
                {
                    var messageBox = MessageBoxManager.GetMessageBoxStandard(
                        "Предупреждение",
                        "Файлы не были переданы. Анализ логов будет пропущен.",
                        ButtonEnum.Ok);
                    
                    await messageBox.ShowAsync();

                    IsAnalyzeActive = false;
                    
                    return;
                }

                try
                {
                    ParseAndAnalyzeResult analyzeResult = await _logAnalyzerClient.AnalyzeFilesAsync(
                        message.FullPathes);

                    CurrentViewModel = new AnalyzeResultTabSwticherViewModel();
                }
                catch (Exception ex)
                {
                    var messageBox = MessageBoxManager.GetMessageBoxStandard(
                        "Ошибка",
                        $"Произошла ошибка при анализе логов:{Environment.NewLine}{ex.ToString()}",
                        ButtonEnum.Ok);
                    
                    await messageBox.ShowAsync();
                }
                finally
                {
                    IsAnalyzeActive = false;
                }
            }));
    }
}