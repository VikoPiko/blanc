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
        public ICommand ShowCategoryCommand { get; }
        public ICommand ShowMenuCommand { get; }
        public ICommand ShowBillCommand { get; }
        public ICommand ShowKitchenCommand { get; }
        public ICommand ShowTablesCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            ShowDashboardCommand = new ViewModelCommands(ExecuteShowDashboardViewCommand);
            ShowStaffCommand = new ViewModelCommands(ExecuteShowStaffViewCommand);
            ShowCategoryCommand = new ViewModelCommands(ExecuteShowCategoryCommand);
            ShowMenuCommand = new ViewModelCommands(ExecuteShowMenuCommand);
            ShowBillCommand = new ViewModelCommands(ExecuteShowBillCommand);
            ShowKitchenCommand = new ViewModelCommands(ExecuteShowKitchenCommand);
            ShowTablesCommand = new ViewModelCommands(ExecuteShowTablesCommand);

            ExecuteShowDashboardViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowTablesCommand(object obj)
        {
            CurrentChildView = new TablesViewModel();
            Caption = "Tables";
            Icon = IconChar.Table;
        }

        private void ExecuteShowKitchenCommand(object obj)
        {
            CurrentChildView = new KitchenViewModel();
            Caption = "Kitchen";
            Icon = IconChar.CodeFork;
        }

        private void ExecuteShowBillCommand(object obj)
        {
            CurrentChildView = new BillViewModel();
            Caption = "Bill";
            Icon = IconChar.CreditCard;
        }
        private void ExecuteShowCategoryCommand(object obj)
        {
            CurrentChildView = new CategoryViewModel();
            Caption = "Category";
            Icon = IconChar.Table;
        }
        private void ExecuteShowMenuCommand(object obj)
        {
            CurrentChildView = new MenuViewModel();
            Caption = "Menu";
            Icon = IconChar.Book;
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
                        DisplayName = $"{user.Username}",//{user.Id}",
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
