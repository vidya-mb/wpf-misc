using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace EnhancedTextApp
{
    public static class TextSuggestionHelper
    {

        public static readonly DependencyProperty TextSuggestionsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "TextSuggestionsEnabled",
                typeof(bool),
                typeof(TextSuggestionHelper),
                new FrameworkPropertyMetadata(false, OnTextSuggestionsEnabled));

        public static bool GetCornerRadius(Control control)
        {
            return (bool)control.GetValue(TextSuggestionsEnabledProperty);
        }

        public static void SetCornerRadius(Control control, CornerRadius value)
        {
            control.SetValue(TextSuggestionsEnabledProperty, value);
        }

        private static void OnTextSuggestionsEnabled(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateTextSuggestionsMenuItemVisibility((FrameworkElement)d);
        }

        private static void UpdateTextSuggestionsMenuItemVisibility(FrameworkElement d)
        {
            if(d == null) return;

            var suggestionsPopup = GetSuggestionsPopupForElement(d);
            var contextMenu = d.ContextMenu;

            
        }

        private static Popup GetSuggestionsPopupForElement(FrameworkElement fe)
        {
            return new Popup();
        }
    }
}
