using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LogAnalyzer.Desktop.ViewModels;
using LogAnalyzer.Desktop.Views;

namespace LogAnalyzer.Desktop;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var homePage = Program.GetRequiredService<MainWindowViewModel>();

            homePage.CurrentViewModel = Program.GetRequiredService<FilesUploadeViewModel>();
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = homePage
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}