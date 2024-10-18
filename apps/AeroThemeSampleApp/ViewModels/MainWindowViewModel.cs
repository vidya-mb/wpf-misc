using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroThemeSampleApp.Navigation;
using AeroThemeSampleApp.Views;

namespace AeroThemeSampleApp.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "WPF Win11 Theme Gallery";

        [ObservableProperty]
        private ICollection<NavigationItem> _controls = new ObservableCollection<NavigationItem>
        {
            new NavigationItem("User Dashboard", typeof(UserDashboardPage)),
        };

        public MainWindowViewModel()
        {

        }
    }
}
