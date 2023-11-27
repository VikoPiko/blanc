using blanc.Models;
using blanc.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using FontAwesome.Sharp;
using System.Security.AccessControl;
using blanc.Views;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace blanc.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private UserAccountModel? _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private IUserRepository userRepository;

        public UserAccountModel? CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }


        public ICommand ShowDashboardCommand { get; }
        public ICommand ShowStaffCommand { get; }
        public ICommand ShowMenuCommand { get; }
        public ICommand ShowBillCommand { get; }
        public ICommand ShowKitchenCommand { get; }
        public ICommand ShowTablesCommand { get; }
        public ICommand ShowSettingsCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            ShowDashboardCommand = new ViewModelCommands(ExecuteShowDashboardViewCommand);
            ShowStaffCommand = new ViewModelCommands(ExecuteShowStaffViewCommand);
            ShowMenuCommand = new ViewModelCommands(ExecuteShowMenuCommand);
            ShowBillCommand = new ViewModelCommands(ExecuteShowBillCommand);
            ShowKitchenCommand = new ViewModelCommands(ExecuteShowKitchenCommand);
            ShowTablesCommand = new ViewModelCommands(ExecuteShowTablesCommand);
            ShowSettingsCommand = new ViewModelCommands(ExecuteShowSettingsCommand);

            ExecuteShowDashboardViewCommand(null);

            LoadCurrentUserData();
        }
        //Binds the ViewModel with the currentChildView, which is binded with the main window to display a given window;
        //FIX -> Everytime a "Window" is clicked a new instance of that window is created so we can't save the data for it(So far) <- FIX
        private void ExecuteShowSettingsCommand(object obj)
        {
            CurrentChildView = new SettingsViewModel();
            Caption = "Settings";
            Icon = IconChar.Gear;
        }

        private void ExecuteShowTablesCommand(object obj)
        {
            CurrentChildView = new TablesViewModel();
            Caption = "Tables";
            Icon = IconChar.List;
        }

        private void ExecuteShowKitchenCommand(object obj)
        {
            CurrentChildView = new KitchenViewModel();
            Caption = "Kitchen";
            Icon = IconChar.Utensils;
        }

        private void ExecuteShowBillCommand(object obj)
        {
            CurrentChildView = new BillViewModel();
            Caption = "Bill";
            Icon = IconChar.CreditCard;
        }

        private void ExecuteShowMenuCommand(object obj)
        {
            CurrentChildView = new MenuViewModel();
            Caption = "Menu";
            Icon = IconChar.BookOpen;
        }

        private void ExecuteShowStaffViewCommand(object obj)
        {
            CurrentChildView = new StaffViewModel();
            Caption = "Staff";
            Icon = IconChar.UserGroup;
        }
        private void ExecuteShowDashboardViewCommand(object? obj)
        {
            CurrentChildView = new DashboardViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void LoadCurrentUserData()
        {
            if (userRepository != null)
            {
                var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
                if (user != null)
                {
                    CurrentUserAccount = new UserAccountModel()
                    {
                        Username = user.Username,
                        DisplayName = $"{user.Username}",//{user.Id}", -> moje s nego moje i bez nego tbh, tva samo izpisva UserID na Main Windowa kato se logne daden user;
                        ProfilePicture = null
                    };
                }
            }
            else
            {
                MessageBox.Show("Invalid user, not logged in");
                Application.Current.Shutdown();
            }
        }

        private ViewModelCommands? logoutCommand;

        public ICommand LogoutCommand => logoutCommand ??= new ViewModelCommands(Logout);


        private void Logout(object commandParameter)
        {

        }
    }
}
