using System.Windows;
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for WindowUser.xaml
    /// </summary>
    public partial class WindowUser : Window
    {

        // 13-14
        public WindowUser()
        {
            InitializeComponent();
            DataContext = new UserViewModel();

        }



    }
}
