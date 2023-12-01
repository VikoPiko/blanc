using blanc.Models;
using System.Windows;
using System.Windows.Controls;
namespace blanc.Views
{
    public partial class TablesView : UserControl
    {
        public TablesView()
        {
            InitializeComponent();
        }

        private void OpenMiniWindow_Click(object sender, RoutedEventArgs e)
        {
            
                MiniTable miniWindow = new MiniTable(this);
                miniWindow.ShowDialog(); // or miniWindow.ShowDialog() if you want it to be modal -> ?????????
            
        }

     
    }
}
