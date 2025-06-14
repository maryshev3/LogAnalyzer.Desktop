using Avalonia;
using Avalonia.ReactiveUI;
using System;
using LogAnalyzer.Desktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace LogAnalyzer.Desktop;

sealed class Program
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public static T GetRequiredService<T>()
        => ServiceProvider.GetRequiredService<T>();
    
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        ServiceProvider = ConfigureServices();
        
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
    
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        
        // Регистрируем сервисы.
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<FilesUploadeViewModel>();
        
        return services.BuildServiceProvider();
    }
}