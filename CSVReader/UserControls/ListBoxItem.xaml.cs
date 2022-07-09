using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CSVReader.UserControls
{
    public partial class ListBoxItem : UserControl
    {
        public ListBoxItem()
        {
            MouseLeftButtonDown += OnMouseLeftButtonDown;
            InitializeComponent();
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (Command != null)
            {
                if (Command.CanExecute(CommandParameter))
                {
                    Command.Execute(CommandParameter);
                }
            }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand),
            typeof(ListBoxItem),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object),
            typeof(ListBoxItem),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
    }
}
