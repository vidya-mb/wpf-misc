using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EnhancedTextApp
{
    /// <summary>
    /// Interaction logic for TextSuggestionsDialogBox.xaml
    /// </summary>
    public partial class TextSuggestionsDialogBox : UserControl
    {
        static TextSuggestionsDialogBox()
        {
            CommandManager.RegisterClassCommandBinding(typeof(TextSuggestionsDialogBox), new CommandBinding(TextSuggestionCommands.CustomRewrite, OnCustomRewrite));
            CommandManager.RegisterClassCommandBinding(typeof(TextSuggestionsDialogBox), new CommandBinding(TextSuggestionCommands.FriendlyRewrite, OnFriendlyRewrite));
            CommandManager.RegisterClassCommandBinding(typeof(TextSuggestionsDialogBox), new CommandBinding(TextSuggestionCommands.ProfessionalRewrite, OnProfessionalRewrite));
            CommandManager.RegisterClassCommandBinding(typeof(TextSuggestionsDialogBox), new CommandBinding(TextSuggestionCommands.ConciseRewrite, OnConciseRewrite));
            CommandManager.RegisterClassCommandBinding(typeof(TextSuggestionsDialogBox), new CommandBinding(TextSuggestionCommands.ElaborateRewrite, OnElaborateRewrite));
        }

        private static void OnConciseRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void OnElaborateRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void OnProfessionalRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void OnFriendlyRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void OnCustomRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public TextSuggestionsDialogBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SuggestionTargetProperty =
            DependencyProperty.Register(
                nameof(SuggestionTarget),
                typeof(UIElement),
                typeof(TextSuggestionsDialogBox),
                new PropertyMetadata(null, OnTextSuggestionTargetChanged));

        private static void OnTextSuggestionTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public UIElement SuggestionTarget
        {
            get => (UIElement)GetValue(SuggestionTargetProperty);
            set => SetValue(SuggestionTargetProperty, value);
        }

        #region Private Methods
        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            var popup = this.Parent as Popup;
            popup.IsOpen = false;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if(tb.Text == "Describe your changes ...")
                {
                    tb.Text = string.Empty;
                }
                tb.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if(tb.Text == string.Empty)
                {
                    tb.Text = "Describe your changes ...";
                    tb.Foreground = new SolidColorBrush(Colors.Gray);
                }
            }
        }

        #endregion
    }
}
