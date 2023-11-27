using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace blanc.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed) 
            {
                DragMove();
            }
        }
        private void btnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnClose_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnLogIn_Click(Object sender, RoutedEventArgs e)
        {
            MainWindow subWindow = new MainWindow();
            subWindow.Show();
            this.Close();
        }
        private void BindablePasswordBox_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
