using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using TestApplication.ViewModel;

namespace TestApplication {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            var mainWindowVM = new MainViewModel();
            var mainWindow = new MainWindow(mainWindowVM);

            mainWindow.Show();
        }
    }
}
