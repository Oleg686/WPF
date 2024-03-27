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
    /// Логика взаимодействия для AddPg.xaml
    /// </summary>
    public partial class AddPg : Page
    {
        public static Students _current = new Students();
        public AddPg(Students students)
        {
            InitializeComponent();
            if(students != null)
            {
                _current = students;
                cmGroup.SelectedIndex = _current.GroupID - 1;
            }
            DataContext = _current;
            cmGroup.ItemsSource = TestEntities.GetContext().Group.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudentPg());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(tnbName.Text)) error.AppendLine("Введите имя!");
            if (string.IsNullOrEmpty(tnbPass.Text)) error.AppendLine("Введите пароль!");
            if (string.IsNullOrEmpty(cmGroup.Text)) error.AppendLine("Введите группу!");
            if(error.Length != 0)
            {
                MessageBox.Show(error.ToString());
            }
            else
            {
                _current.GroupID = cmGroup.SelectedIndex + 1;
                if (_current.ID == 0)
                    TestEntities.GetContext().Students.Add(_current);
                try
                {
                    TestEntities.GetContext().SaveChanges();
                    MessageBox.Show("Пользователь успешно добавлен");
                    NavigationService.Navigate(new StudentPg());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
