using System.Windows;
using WpfApp1.View;
using WpfApp1.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new UserViewModel();
            DataContext = new ProductViewModel();
        }
        private void Employee_OnClick(object sender, RoutedEventArgs e)
        {
            WindowEmployee wEmployee = new WindowEmployee();
            wEmployee.Show();
        }
        private void User_OnClick(object sender, RoutedEventArgs e)
        {
            WindowUser wUser = new WindowUser();
            wUser.Show();
        }

        // 13-14
        public static int IdUser { get; set; }

        public static int IdEmployee { get; set; }
    }
}