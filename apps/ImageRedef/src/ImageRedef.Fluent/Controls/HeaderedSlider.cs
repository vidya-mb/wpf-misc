using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace ImageRedef.Fluent.Controls
{
    [DefaultEvent("ValueChanged"), DefaultProperty("Value")]
    [TemplatePart(Name = "PART_ValueTextBlock", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_Slider", Type = typeof(Slider))]
    class HeaderedSlider : Control
    {
        static HeaderedSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HeaderedSlider), new FrameworkPropertyMetadata(typeof(HeaderedSlider)));
        }

        public static readonly DependencyProperty MinimumProperty
            = DependencyProperty.Register("Minimum", typeof(double),
                typeof(HeaderedSlider), new FrameworkPropertyMetadata(-100.0d));

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty
            = DependencyProperty.Register("Maximum", typeof(double),
                typeof(HeaderedSlider), new FrameworkPropertyMetadata(100.0d));

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty
            = DependencyProperty.Register("Value", typeof(double),
                typeof(HeaderedSlider), new FrameworkPropertyMetadata(0.0d, 
                                        new PropertyChangedCallback(OnValueChanged)));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is HeaderedSlider hs)
            {
                hs.UpdateSelectionRangeAndText((double)e.OldValue, (double)e.NewValue);
                RoutedPropertyChangedEventArgs<double> args = new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue);
                args.RoutedEvent = ValueChangedEvent;
                hs.RaiseEvent(args);
            }
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty
            = DependencyProperty.Register("Header", typeof(object),
                typeof(HeaderedSlider), new PropertyMetadata(null));

        public object Header
        {
            get => GetValue(HeaderProperty);
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(HeaderedSlider));
        public event RoutedPropertyChangedEventHandler<double> ValueChanged { add { AddHandler(ValueChangedEvent, value); } remove { RemoveHandler(ValueChangedEvent, value); } }

        private const string SliderName = "PART_Slider";
        private const string ValueTextBlockName = "PART_ValueTextBlock";

        private Slider? _slider = null;
        private TextBlock? _valueTextBlock = null;

        internal Slider? Slider
        {
            get { return _slider; }
            set 
            { 
                if(_slider != null)
                {
                    _slider.ValueChanged -= OnInternalSliderValueChanged;
                }

                _slider = value;
                
                if (_slider != null)
                {
                    _slider.ValueChanged += OnInternalSliderValueChanged;
                }
            }
        }

        internal TextBlock? ValueTextBlock
        {
            get { return _valueTextBlock; }
            set { _valueTextBlock = value; }
        }

        private void OnInternalSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Value = e.NewValue;
        }

        private void UpdateSelectionRangeAndText(double oldValue, double newValue)
        {
            if (Slider is not null)
            {
                double midValue = (Minimum + Maximum) / 2;
                if (newValue < midValue)
                {
                    Slider.SelectionStart = newValue;
                    Slider.SelectionEnd = midValue;
                }
                else
                {
                    Slider.SelectionStart = midValue;
                    Slider.SelectionEnd = newValue;
                }
            }

            if (ValueTextBlock is not null)
            {
                ValueTextBlock.Text = ((int)newValue).ToString();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Slider = GetTemplateChild(SliderName) as Slider;
            ValueTextBlock = GetTemplateChild(ValueTextBlockName) as TextBlock;
        }
    }
}
