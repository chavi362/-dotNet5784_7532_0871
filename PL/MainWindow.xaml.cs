using PL.Engineer;
using PL.Task;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HandleToEngineersScreen(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }

        private void Initialization(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to perform the initialization?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // User clicked "Yes," call the initialization method
                DalTest.Initialization.Do();
            }
        }

        private void GoToTasks(object sender, RoutedEventArgs e)
        {
            new TaskListWindow().Show();
        }
    }
}
