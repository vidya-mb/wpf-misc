using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EnhancedTextApp
{
    public class TextBoxAdorner : Adorner
    {
        public TextBoxAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _aiButton = new Button {
                                //Content = new TextBlock
                                //{
                                //    FontFamily = new FontFamily("Segoe Fluent Icons"),
                                //    FontSize = 18,
                                //    Text = "\uE720",
                                //    VerticalAlignment = VerticalAlignment.Center,
                                //    HorizontalAlignment = HorizontalAlignment.Center
                                //},
                                Content = new Image
                                {
                                    Source = new BitmapImage(new Uri("pack://application:,,,/copilot-icon.png")),
                                    Width=20,
                                    Height=20,
                                },
                                Width = 30,
                                Height = 30,
                                BorderBrush = Brushes.Transparent,
                                Background = Brushes.Transparent,
                                Padding = new Thickness(10),
                                VerticalAlignment = VerticalAlignment.Center,
                            };
            _aiButton.Click += OnAIButtonClick;
            AddVisualChild(_aiButton);
            AddLogicalChild(_aiButton);
        }

        private void OnAIButtonClick(object sender, RoutedEventArgs e)
        {
            AIButtonClicked?.Invoke(this, e);
        }

        public event RoutedEventHandler AIButtonClicked;

        private Button _aiButton;

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            return _aiButton;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _aiButton.Measure(constraint);
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (AdornedElement is TextBoxBase tbb)
            {
                _aiButton.Arrange(new Rect(finalSize.Width - _aiButton.Width - 2, finalSize.Height - _aiButton.Height - 2, _aiButton.Width, _aiButton.Height));
            }
            return finalSize;
        }
    }
}
