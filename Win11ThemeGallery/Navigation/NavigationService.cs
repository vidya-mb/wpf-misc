


using System.Collections;
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
        if( type == null ) return;
        var page = _serviceProvider.GetRequiredService(type);
        _frame.Navigate(page);
    }
}

public class NavigationItem
{
    public string Name { get; set; } = "";
    public Type? PageType { get; set; } = null;

    public ICollection<NavigationItem> Children { get; set; } = new ObservableCollection<NavigationItem>();

    public NavigationItem() { }

    public NavigationItem(string name, Type pageType)
    {
        Name = name;
        PageType = pageType;
    }

    public NavigationItem(string name, Type pageType, ObservableCollection<NavigationItem> navItems)
    {
        Name = name;
        PageType = pageType;
        Children = navItems;
    }

    public override string ToString()
    {
        return Name;
    }
}