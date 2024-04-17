using System.Windows;
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for WindowEmployee.xaml
    /// </summary>
    public partial class WindowEmployee : Window
    {


        public WindowEmployee()
        {
            InitializeComponent();
            DataContext = new ProductViewModel();
        }


    }
}
