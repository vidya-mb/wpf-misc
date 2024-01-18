namespace Win11ThemeGallery.Navigation
{
    public class NavigationOccuredEventArgs
    {
        public Type? PageType { get; set; } = null;

        public NavigationOccuredEventArgs() { }

        public NavigationOccuredEventArgs(Type pageType)
        {
            PageType = pageType;
        }
    }
}