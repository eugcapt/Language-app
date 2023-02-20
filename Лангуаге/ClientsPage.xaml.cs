﻿using System;
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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            MainDataGrid.ItemsSource = user2Entities.GetContext().Client.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPage(MainDataGrid.SelectedItem as Client));
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Действительно желаете удалить данные?", "ВНИМАНИЕ!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    user2Entities.GetContext().Client.Remove(MainDataGrid.SelectedItem as Client);  
                    user2Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    MainDataGrid.ItemsSource = user2Entities.GetContext().Client.ToList();   
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MainDataGrid.ItemsSource = user2Entities.GetContext().Client.ToList();
        }
    }
}
