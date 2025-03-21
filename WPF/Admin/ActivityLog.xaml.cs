using DataAssetObjects;
using Repositories.Repository;
using Services.Service;
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

namespace WPF.Admin
{
    /// <summary>
    /// Interaction logic for ActivityLog.xaml
    /// </summary>
    public partial class ActivityLog : Window
    {
        private int _id;
        private readonly ActivityLoggerService _activityLoggerService;
        private readonly HrmanagementContext _context;

        public ActivityLog(int id)
        {
            var context = new HrmanagementContext();
            _activityLoggerService = new ActivityLoggerService(new ActivityLoggerReposirory(new ActivityLoggerDAO(context)));
            InitializeComponent();
            LoadActivityLogs();
        }

        private void LoadActivityLogs()
        {
            var logs = _activityLoggerService.GetAllActivityLogs();
            lvActivityLogs.ItemsSource = logs;
        }
    }
}
