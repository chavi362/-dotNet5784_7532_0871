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
using System.Windows.Shapes;

namespace PL.Milestone
{
    public partial class MilestoneListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public IEnumerable<BO.Milestone> MilestoneList
        {
            get { return (IEnumerable<BO.Milestone>)GetValue(MilestoneListProperty); }
            set { SetValue(MilestoneListProperty, value); }
        }

        public static readonly DependencyProperty MilestoneListProperty =
            DependencyProperty.Register("MilestoneList", typeof(IEnumerable<BO.Milestone>), typeof(MilestoneListWindow), new PropertyMetadata(null));
        public BO.Status TStatus { get; set; } = BO.Status.None;
        public MilestoneListWindow()
        {
            InitializeComponent();
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            MilestoneList = s_bl?.Task.ReadAll((task)=>task.Milestone==null).Select(task => s_bl?.Milestone.Read(task.Id))!;
        }
        private void AddMilestoneClick(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
        }

        private void DubbleClickMilestone(object sender, MouseButtonEventArgs e)
        {
            BO.Milestone? task = (sender as ListView)?.SelectedItem as BO.Milestone;
            new TaskWindow(task!.Id).ShowDialog();
        }


        private void SearchByStatus(object sender, SelectionChangedEventArgs e)
        {
            MilestoneList = (TStatus == BO.Status.None) ?
          s_bl?.Task!.ReadAll((task) => task.Milestone == null).Select(task => s_bl?.Milestone.Read(task.Id))! : s_bl?.Task.ReadAll((task) => task.Milestone == null && task.Status==TStatus).Select(task => s_bl?.Milestone!.Read(task.Id))!;
        }
    }
}
