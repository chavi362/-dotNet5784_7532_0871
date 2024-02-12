using System;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;


namespace PL.Task
{
    /// <summary>
    /// Interaction logic for EnginersListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));
        public BO.Status TStatus { get; set; } = BO.Status.None;
        public TaskListWindow()
        {
            InitializeComponent();
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            QueryTaskList();
        }
        private void QueryTaskList()
        {
            TaskList = s_bl?.TaskInList.ReadAll()!;
        }

        private void AddTaskClick(object sender, RoutedEventArgs e)
        {
           new TaskWindow().ShowDialog();
        }

        private void DubbleClickTask(object sender, MouseButtonEventArgs e)
        {
            BO.TaskInList? task = (sender as ListView)?.SelectedItem as BO.TaskInList;
            new TaskWindow(task!.Id).ShowDialog();
        }

        private void SearchByStatus(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (TStatus == BO.Status.None) ?
          s_bl?.TaskInList.ReadAll()! : s_bl?.TaskInList.ReadAll(item => item.Status == TStatus)!;
        }
    }
}
