using blanc.Models;
using blanc.Repositories;
using blanc.Views;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Windows.Input;

namespace blanc.ViewModels
{
    internal class LoginViewModel : ViewModelBase
    {
        private string? _username;
        private SecureString _password;
        private string? _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository? userRepository;
        private bool _isLoggedIn;

        public bool IsLoggedIn 
        {
            get { return _isLoggedIn; }
            set 
            {
                _isLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }
        public string? Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password 
        {
            get {
                return _password ?? new SecureString();
             }
            set 
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string? ErrorMessage 
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible 
        {
            get { return _isViewVisible; }
            set 
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommands(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3
                || Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else 
            {
                ErrorMessage = " * Invalid Username or password";
            }
        }
    }
}
