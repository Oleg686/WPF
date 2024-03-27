using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPg.xaml
    /// </summary>
    public partial class AuthPg : Page
    {
        public AuthPg()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            var user = TestEntities.GetContext().Students.AsNoTracking().FirstOrDefault(x => x.Name.ToLower() == txbName.Text.ToLower() && x.Password.ToLower() == pswPass.Password.ToLower());
            NavigationService.Navigate(new StudentPg());
        }
    }
}
