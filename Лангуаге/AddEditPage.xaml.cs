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

namespace Лангуаге
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Client _currentClient = new Client();


        public AddEditPage(Client selectedClient)
        {
            InitializeComponent();

            if (selectedClient != null)
            {
                _currentClient = selectedClient;
                AddEditTextBlock.Text = "Редактирование клиента";
                IDTextBox.IsEnabled = false;
            }
            else
            {
                AddEditTextBlock.Text = "Редактирование клиента";
                IDTextBox.Visibility = Visibility.Hidden;
                
            }


            DataContext = _currentClient;
            GenderComboBox.ItemsSource = user2Entities.GetContext().Gender.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentClient.LastName))
                errors.AppendLine("client's lastname didn't set");
            if (string.IsNullOrWhiteSpace(_currentClient.FirstName))
                errors.AppendLine("client's firstname didn't set");
            if (_currentClient.Gender == null)
                errors.AppendLine("Gender unseted");
            if (_currentClient.Phone == null)
                errors.AppendLine("phone unset");
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentClient.ID == 0)
            {
                user2Entities.GetContext().Client.Add(_currentClient);
            }

            try
            {
                user2Entities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
