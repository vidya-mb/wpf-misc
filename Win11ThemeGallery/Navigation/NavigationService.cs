


using System.Windows.Controls;

namespace Win11ThemeGallery.Navigation;

public interface INavigationService
{
    void NavigateTo(Type type);

    void SetFrame(Frame frame);
}


public class NavigationService : INavigationService
{
    private Frame _frame;
    private readonly IServiceProvider _serviceProvider;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void SetFrame(Frame frame)
    {
        _frame = frame;
    }

    public void NavigateTo(Type type)
    {
        var page = _serviceProvider.GetRequiredService(type);
        _frame.Navigate(page);
    }
}

public record class NavigationItem(string Name, Type PageType);