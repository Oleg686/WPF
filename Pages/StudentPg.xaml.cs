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
    /// Логика взаимодействия для StudentPg.xaml
    /// </summary>
    public partial class StudentPg : Page
    {
        public StudentPg()
        {
            InitializeComponent();
            var temp = TestEntities.GetContext().Group.ToList();
            Group all = new Group();
            all.Name = "Все элементы";
            temp.Insert(0, all);
            cmbFill.ItemsSource = temp;
            cmbFill.SelectedIndex = 0;
            dtgStudent.ItemsSource = TestEntities.GetContext().Students.ToList();
        }

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPg((sender as Button).DataContext as Students));
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void cmbFill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var temp = dtgStudent.SelectedItems.Cast<Students>().ToList();
            if (MessageBox.Show($"Вы точно хотети удалить {temp.Count} пользователей", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                TestEntities.GetContext().Students.RemoveRange(temp);
                TestEntities.GetContext().SaveChanges();
                dtgStudent.ItemsSource = TestEntities.GetContext().Students.ToList();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPg(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TestEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            TestEntities.GetContext().SaveChanges();
            dtgStudent.ItemsSource = TestEntities.GetContext().Students.ToList();
        }
        private void Update()
        {
            var temp = TestEntities.GetContext().Students.ToList();
            if(cmbFill.SelectedIndex != 0)
            {
                temp = temp.Where(x => x.GroupID == cmbFill.SelectedIndex).ToList();
            }
            if(!string.IsNullOrEmpty(txbSearch.Text))
            {
                temp = temp.Where(x => x.Name.ToLower().Contains(txbSearch.Text.ToLower())).ToList();
            }
            dtgStudent.ItemsSource = temp;
        }
    }
}
