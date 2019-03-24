using SecureChatApp.View;
using SecureChatApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SecureChatApp
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var app = new MainWindow();
            //var context = new MainViewModel();
            //app.DataContext = context;
            app.Show();
        }
    }
}
