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
using System.Windows.Shapes;
using System.Diagnostics;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EnginersListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
     
        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));
        public BO.EngineerExperience EExperience { get; set; } = BO.EngineerExperience.None;
        public EngineerListWindow()
        {
            InitializeComponent();
            //EngineerList = s_bl?.Engineer.ReadAll()!;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            QueryEngineerList();
        }
        private void QueryEngineerList()
        {
            EngineerList = s_bl?.Engineer.ReadAll()!;
        }

        private void SearchByLevel(object sender, SelectionChangedEventArgs e)
        {
           
            EngineerList = (EExperience == BO.EngineerExperience.None) ?
            s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == EExperience)!;
        }

        private void AddEngineerClick(object sender, RoutedEventArgs e)
        {
            new EngineerWIndow().ShowDialog();
        }

        private void DubbleClickEngineer(object sender, MouseButtonEventArgs e)
        {
            BO.Engineer? engineer = (sender as ListView)?.SelectedItem as BO.Engineer;
            new EngineerWIndow(engineer!.Id).ShowDialog();
        }
    }
    //private void EngineerListWindow_Activated(object sender, EventArgs e)
    //{
    //    var temp = s_bl?.Engineer.ReadAll();
    //    EngineerList = (temp == null) ? new() : new(temp!);
    //    var updatedEngineer = s_bl?.Engineer.ReadAll();
    //    EngineerList = updatedEngineer == null ? new() : new(updatedEngineer!);
    //}
}
