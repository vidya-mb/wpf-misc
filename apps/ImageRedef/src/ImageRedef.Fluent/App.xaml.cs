using ImageRedef.Fluent.ViewModels;
using ImageRedef.Fluent.Views;

namespace ImageRedef.Fluent;

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
            services.AddTransient<ImageGalleryPage>();
            services.AddTransient<ProImageGalleryViewModel>();
            services.AddTransient<PhotoEditorWindow>();
            services.AddTransient<PhotoEditorViewModel>();

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

