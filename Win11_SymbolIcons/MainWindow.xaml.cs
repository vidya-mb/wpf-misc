using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Win11_SymbolIcons
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GenerateAllSymbolIcons();
        }

        private void GenerateAllSymbolIcons()
        {
            var symbols = Enum.GetValues(typeof(SymbolRegular));
            int i = 0;
            foreach (var symbol in symbols)
            {
                var symbolIcon = new SymbolIcon()
                {
                    Symbol = (SymbolRegular)symbol,
                    Margin = new Thickness(5),
                    Width = 50,
                    Height = 50,
                };
                symbolIcon.ToolTip = symbol.ToString();
                IconPanel.Children.Add(symbolIcon);
                i++;
                if (i == 200) break;
            }
        }
    }
}