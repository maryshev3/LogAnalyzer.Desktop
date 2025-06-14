namespace LogAnalyzer.Desktop.ViewModels;
using ReactiveUI;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
    }

    public MainWindowViewModel()
    {
        
    }
}