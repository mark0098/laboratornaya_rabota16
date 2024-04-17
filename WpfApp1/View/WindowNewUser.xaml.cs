using System.Windows;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for WindowNewUser.xaml
    /// </summary>
    public partial class WindowNewUser : Window
    {
        public WindowNewUser()
        {
            InitializeComponent();
        }
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
