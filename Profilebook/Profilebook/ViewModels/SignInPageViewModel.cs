using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using Profilebook.Tables;
using Prism.Commands;
using Profilebook.Services.SettingsManager;
using System.ComponentModel;

namespace Profilebook.ViewModels
{
    public class SignInPageViewModel : BindableBase, INavigatedAware
    {

        
        public SignInPageViewModel(ISettingsManager settingsManager, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _settingsManager = settingsManager;
            IsAuthorised = _settingsManager.IsAuthorised;
        }

        #region --- Public properties ---

        private string _signInLogin;
        public string SignInLogin
        {
            get => _signInLogin;
            set => SetProperty(ref _signInLogin, value);
        }


        private string _signInPassword;
        public string SignInPassword
        {
            get => _signInPassword;
            set => SetProperty(ref _signInPassword, value);
        }


        private bool _isAuthorised;
        public bool IsAuthorised
        {
            get => _isAuthorised;
            set => SetProperty(ref _isAuthorised, value);
        }


        public bool IsEnabled { get; private set; }

        public ICommand SignUpHyperlinkCommand => new Command(SignUpHyperlink);

        private DelegateCommand _signInButtonCommand;
        public DelegateCommand SignInButtonCommand =>
            _signInButtonCommand ??
            (_signInButtonCommand = new DelegateCommand(SignInButton)).ObservesCanExecute(() => IsEnabled);

        #endregion

        #region --- Services --- 

        private ISettingsManager _settingsManager;
        INavigationService _navigationService;

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            SignInLogin = (string)parameters["LoginParameter"];
        }

        #endregion

        #region --- Overrides ---
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(IsAuthorised))
            {
                _settingsManager.IsAuthorised = IsAuthorised;
            }
        }

        #endregion

        #region --- Private Helpers ---
        private async void SignUpHyperlink()
        {
            await _navigationService.NavigateAsync("SignUpPage");
        }

        private async void SignInButton()
        {

            var IsUsernameTaken = App.UsersDatabase.UsernameCheck(SignInLogin);

            if (!IsUsernameTaken)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("This user doesn't exist");
            }

            else
            {
                var IsUserAuthorised = App.UsersDatabase.ValidateUser(SignInLogin, SignInPassword);

                if (IsUserAuthorised)
                {
                    IsAuthorised = true;
                    await _navigationService.NavigateAsync(new System.Uri("http://www.Profilebook/NavigationPage/MainList", System.UriKind.Absolute));
                }

                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Invalid password");
                    SignInPassword = "";
                }
            }
        }

        #endregion
    }
}
