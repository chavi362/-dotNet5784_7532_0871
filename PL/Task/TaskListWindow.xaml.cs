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

        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));
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
            TaskList = s_bl?.Task.ReadAll()!;
        }

        private void AddTaskClick(object sender, RoutedEventArgs e)
        {
           new TaskWindow().ShowDialog();
        }

        private void DubbleClickTask(object sender, MouseButtonEventArgs e)
        {
            BO.Task? task = (sender as ListView)?.SelectedItem as BO.Task;
            new TaskWindow(task!.Id).ShowDialog();
        }

        private void SearchByStatus(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (TStatus == BO.Status.None) ?
          s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Status == TStatus)!;
        }
    }
}
