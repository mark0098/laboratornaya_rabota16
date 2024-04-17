using System.Windows;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for WindowNewEmployee.xaml
    /// </summary>
    public partial class WindowNewProducts : Window
    {
        public WindowNewProducts()
        {
            InitializeComponent();
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
