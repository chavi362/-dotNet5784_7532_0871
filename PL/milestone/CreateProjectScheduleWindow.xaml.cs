using BlApi;
using System;
using System.ComponentModel;
using System.Windows;

namespace PL.milestone
{
    public partial class CreateProjectScheduleWindow : Window, IDataErrorInfo
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        // Add properties for StartDate and EndDate
        // Add properties for StartDate and EndDate
        private DateTime? _startDate;
        private DateTime? _endDate;
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public CreateProjectScheduleWindow()
        {
            InitializeComponent();
            // Set DataContext to the current instance (this) to enable data binding
            DataContext = this;
        }

        private void CreateProjectSchedule_Click(object sender, RoutedEventArgs e)
        {
            
            s_bl.Milestone.SetStartDate(_startDate);
            s_bl.Milestone.SetEndDate(_endDate);
            try
            {
                s_bl.Milestone.CreateProjectSchedule();

                MessageBox.Show($"project shedule  was successfully created!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex) {
                MessageBox.Show($"project shedule  creating failed{ex}", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
               }
          
            this.Close();
        }

        public string? this[string columnName]
        {
            get
            {
                if (columnName == "StartDate" && StartDate == null)
                {
                    return "Start date is required.";
                }
                else if (columnName == "EndDate" && EndDate == null)
                {
                    return "End date is required.";
                }

                return null;
            }
        }

        public string Error => null!;
    }
}
