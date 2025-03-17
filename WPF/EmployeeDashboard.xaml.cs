using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    public partial class EmployeeDashboard : Window, INotifyPropertyChanged
    {
        private object _currentView;

        public EmployeeDashboard()
        {
            InitializeComponent();

            this.DataContext = this;

            CurrentView = null;
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        private void ReportEmployee_Click(object sender, RoutedEventArgs e)
        {
            CurrentView = new EmployeeManager();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
