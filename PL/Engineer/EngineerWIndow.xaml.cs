using BO;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL.Engineer
{
    public partial class EngineerWIndow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
       // public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;
        public BO.Engineer? Engineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        public static readonly DependencyProperty EngineerProperty =
            DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWIndow), new PropertyMetadata(null));
        public EngineerWIndow(int id = 0)
        {
            InitializeComponent();
            try {
                EngineerExperience level;
                Enum.TryParse<EngineerExperience>(0.ToString(), out level);
                Engineer = id != 0 ? s_bl.Engineer.Read(id) : new BO.Engineer { Id = 0, Name = "", Email = "" ,Level=level }; 
            }
            catch (BO.BlDoesNotExistException ex)
            {
                Engineer = null;
                MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void AddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)!.Content.ToString() == "Add")
            {
                try
                {
 

                    int? id = s_bl.Engineer.Create(Engineer!);
                    MessageBox.Show($"Engineer {id} was successfully added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (BO.BlAlreadyExistsException ex)
                {
                    MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else //Update
            {
                try
                {
                  

                    s_bl.Engineer.Update(Engineer!);
                    MessageBox.Show($"Engineer {Engineer?.Id} was successfully updated!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (BO.BlDoesNotExistException ex)
                {
                    MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Operation Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
    }
}
