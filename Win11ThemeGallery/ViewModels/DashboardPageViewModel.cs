using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Win11ThemeGallery.Navigation;
using Win11ThemeGallery.Views;

namespace Win11ThemeGallery.ViewModels
{
    public partial class DashboardPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ICollection<NavigationCard> _navigationCards = new ObservableCollection<NavigationCard>
        {
            new NavigationCard
            {
                Name = "Basic Input",
                PageType = typeof(BasicInputPage),
                Icon = new SymbolIcon { Symbol = SymbolRegular.CheckboxChecked24 },
                Description = "Button, CheckBox, Slider..."
            },
            new NavigationCard
            {
                Name = "Collections",
                PageType = typeof(CollectionsPage),
                Icon = new SymbolIcon { Symbol = SymbolRegular.Table24 },
                Description = "DataGrid, ListBox..."
            },
            new NavigationCard
            {
                Name = "Date & Time",
                PageType = typeof(DateAndTimePage),
                Icon = new SymbolIcon { Symbol = SymbolRegular.CalendarClock24 },
                Description = "Calendar, DatePicker"
            },
            new NavigationCard
            {
                Name = "Layout",
                PageType = typeof(LayoutPage),
                Icon = new SymbolIcon { Symbol = SymbolRegular.News24 },
                Description = "Expander"
            },
            // new NavigationCard
            // {
            //     Name = "Media",
            //     PageType = typeof(MediaPage),
            //     Icon = new SymbolIcon { Symbol = SymbolRegular.PlayCircle24 },
            //     Description = "Canvas, Image"
            // },
            new NavigationCard
            {
                Name = "Navigation",
                PageType = typeof(NavigationPage),
                Icon = new SymbolIcon { Symbol = SymbolRegular.Navigation24 },
                Description = "Menu, TabControl"
            },
            new NavigationCard
            {
                Name = "Status & Info",
                PageType = typeof(StatusAndInfoPage),
                Icon = new SymbolIcon { Symbol = SymbolRegular.ChatBubblesQuestion24 },
                Description = "ProgressBar, ToolTip"
            },
            new NavigationCard
            {
                Name = "Text",
                PageType = typeof(TextPage),
                Icon = new SymbolIcon { Symbol = SymbolRegular.DrawText24 },
                Description = "Label, TextBox, ..."
            },
        };

        private readonly INavigationService _navigationService;

        public DashboardPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        public void Navigate(object pageType){
            if (pageType is Type page)
            {
                _navigationService.NavigateTo(page);
            }
        }

        
    }
}
