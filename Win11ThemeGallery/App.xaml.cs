using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Win11ThemeGallery;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    private static readonly IHost _host = Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddTransient<ButtonPage>();
            services.AddTransient<ButtonPageViewModel>();
        }).Build();


    [STAThread]
    public static void Main()
    {
        _host.Start();

        App app = new();
        app.InitializeComponent();
        app.MainWindow = _host.Services.GetRequiredService<MainWindow>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();
    }
}

