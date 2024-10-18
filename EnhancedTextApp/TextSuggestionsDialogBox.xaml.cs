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
using static EnhancedTextApp.TextSuggestionCommandHelpers;

namespace EnhancedTextApp
{
    /// <summary>
    /// Interaction logic for TextSuggestionsDialogBox.xaml
    /// </summary>
    public partial class TextSuggestionsDialogBox : UserControl
    {
        static TextSuggestionsDialogBox()
        {
            CommandManager.RegisterClassCommandBinding(typeof(TextSuggestionsDialogBox), new CommandBinding(TextSuggestionCommands.CustomRewrite, ExecuteCustomRewrite));
            CommandManager.RegisterClassCommandBinding(typeof(TextSuggestionsDialogBox), new CommandBinding(TextSuggestionCommands.FriendlyRewrite, ExecuteFriendlyRewrite));
            CommandManager.RegisterClassCommandBinding(typeof(TextSuggestionsDialogBox), new CommandBinding(TextSuggestionCommands.ProfessionalRewrite, ExecuteProfessionalRewrite));
            CommandManager.RegisterClassCommandBinding(typeof(TextSuggestionsDialogBox), new CommandBinding(TextSuggestionCommands.ConciseRewrite, ExecuteConciseRewrite));
            CommandManager.RegisterClassCommandBinding(typeof(TextSuggestionsDialogBox), new CommandBinding(TextSuggestionCommands.ElaborateRewrite, ExecuteElaborateRewrite));
        }

        public TextSuggestionsDialogBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SuggestionTargetProperty =
            DependencyProperty.Register(
                nameof(SuggestionTarget),
                typeof(UIElement),
                typeof(TextBoxBase),
                new PropertyMetadata(null, OnTextSuggestionTargetChanged));

        private static void OnTextSuggestionTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue != null)
            {
                var suggestionDialogBox = d as TextSuggestionsDialogBox;
                var element = e.NewValue as UIElement;

                if (suggestionDialogBox is null) return;

                suggestionDialogBox.ConciseRewriteButton.CommandTarget = element;
                suggestionDialogBox.ElaborateRewriteButton.CommandTarget = element;
                suggestionDialogBox.CustomRewriteButton.CommandTarget = element;
                suggestionDialogBox.FriendlyRewriteButton.CommandTarget = element;
                suggestionDialogBox.ProfessionalRewriteButton.CommandTarget = element;
            }

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
                if(tb.Text == "Give custom instructions ...")
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
                    tb.Text = "Give custom instructions ...";
                    tb.Foreground = new SolidColorBrush(Colors.Gray);
                }
            }
        }

        #endregion
    }
}
