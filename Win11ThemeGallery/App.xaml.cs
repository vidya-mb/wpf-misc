using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Win11ThemeGallery.Navigation;
using Win11ThemeGallery.ViewModels;
using Win11ThemeGallery.Views;

namespace Win11ThemeGallery;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    private static readonly IHost _host = Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddTransient<ButtonPage>();
            services.AddTransient<ButtonPageViewModel>();
            services.AddTransient<CheckBoxPage>();
            services.AddTransient<CheckBoxPageViewModel>();
            services.AddTransient<ComboBoxPage>();
            services.AddTransient<ComboBoxPageViewModel>();
            services.AddTransient<RadioButtonPage>();
            services.AddTransient<RadioButtonPageViewModel>();
            services.AddTransient<SliderPage>();
            services.AddTransient<SliderPageViewModel>();
            services.AddTransient<CalendarPage>();
            services.AddTransient<CalendarPageViewModel>();
            services.AddTransient<DatePickerPage>();
            services.AddTransient<DatePickerPageViewModel>();
            services.AddTransient<TabControlPage>();
            services.AddTransient<TabControlPageViewModel>();
            services.AddTransient<ProgressBarPage>();
            services.AddTransient<ProgressBarPageViewModel>();
            services.AddTransient<MenuPage>();
            services.AddTransient<MenuPageViewModel>();
            services.AddTransient<ToolTipPage>();
            services.AddTransient<ToolTipPageViewModel>();


            services.AddSingleton<SettingsPage>();
            services.AddSingleton<SettingsPageViewModel>();
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

