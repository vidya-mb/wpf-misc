using System.Windows.Controls.Primitives;

namespace ImageRedef.Fluent.Controls
{
    public class DropDownButton : ComboBox
    {
        static DropDownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
        }

        public static readonly DependencyProperty HeaderProperty = 
            DependencyProperty.Register(nameof(Header), typeof(object),
                typeof(DropDownButton), new PropertyMetadata(null));

        public object Header
        {
            get => (object)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty HeaderCommandProperty =
            DependencyProperty.Register(nameof(HeaderCommand), typeof(ICommand),
                typeof(DropDownButton), new PropertyMetadata(null));

        public ICommand HeaderCommand
        {
            get => (ICommand)GetValue(HeaderCommandProperty);
            set => SetValue(HeaderCommandProperty, value);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
        
            if(SelectedItem is DropDownButtonItem dbi)
            {
                Header = dbi.Content;
                HeaderCommand = dbi.Command;
            }
        }
    }

    public class DropDownButtonItem : ComboBoxItem
    {
        public DropDownButtonItem() : base()
        {

        }

        static DropDownButtonItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButtonItem), new FrameworkPropertyMetadata(typeof(ComboBoxItem)));
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(DropDownButtonItem), new PropertyMetadata(null));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public DropDownButton? ParentDropDownParent { get; private set; }

    }
}
