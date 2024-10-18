using System.Configuration;
using System.Data;
using System.Windows;
using AeroThemeSampleApp.ViewModels;
using AeroThemeSampleApp.Views;

namespace AeroThemeSampleApp;

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

            services.AddTransient<UserDashboardPage>();
            services.AddTransient<UserDashboardPageViewModel>();

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

