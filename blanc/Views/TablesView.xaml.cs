using blanc.Models;
using blanc.ViewModels;
using System.Windows;
using System.Windows.Controls;
namespace blanc.Views
{
    public partial class TablesView : UserControl
    {
        public TablesView()
        {
            InitializeComponent();
            this.DataContext = new TablesViewModel();
        }

        private void OpenMiniWindow_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is TableModel table)
            {
                MiniTable miniWindow = new MiniTable();
                miniWindow.ShowDialog();
            }
        }

    }
}
