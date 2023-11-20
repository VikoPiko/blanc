using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using blanc.Views;

namespace blanc
{
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var LoginWindow = new LoginWindow();
            LoginWindow.Show();
            LoginWindow.IsVisibleChanged += (s, ev) =>
            {
                if (LoginWindow.IsVisible == false && LoginWindow.IsLoaded)
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    LoginWindow.Close();
                }
            };
        }
    }
}
