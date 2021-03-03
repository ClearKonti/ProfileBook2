using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Profilebook.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Profilebook.Services.SettingsManager;
using Xamarin.Forms;

namespace Profilebook.ViewModels
{
    public class SignUpPageViewModel : BindableBase
    {
        public SignUpPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        #region --- Public Properties ---

        

        public ICommand SignUpProcessCommand => new Command(SignUpProcess);
        public ICommand BackToSignInCommand => new Command(BackToSignIn);

        private string _signUpLogin;
        public string SignUpLogin
        {
            get => _signUpLogin;
            set => SetProperty(ref _signUpLogin, value);
        }

        private string _signUpPassword;
        public string SignUpPassword
        {
            get => _signUpPassword;
            set => SetProperty(ref _signUpPassword, value);
        }

        private string _signUpPasswordConfirm;
        public string SignUpPasswordConfirm
        {
            get => _signUpPasswordConfirm;
            set => SetProperty(ref _signUpPasswordConfirm, value);
        }

        #endregion

        #region --- Services --- 

        INavigationService _navigationService;

        #endregion

        #region --- Private Helpers ---

        private async void BackToSignIn()
        {
            await _navigationService.GoBackAsync();
        }
        private async void SignUpProcess()
        {
            if (SignUpLogin.Length < 4 || SignUpLogin.Length > 16)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Username should be at least 4 and not longer than 16 symbols");
            }

            if (SignUpPassword.Length < 8 || SignUpPassword.Length > 16)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Password should be at least 8 and not longer than 16 symbols");
            }

            else
            {
                var IsUsernameTaken = App.UsersDatabase.UsernameCheck(SignUpLogin);
                if (IsUsernameTaken)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("This username is already taken");
                }

                else
                {
                    if (SignUpPassword == SignUpPasswordConfirm)
                    {
                        var newUser = new User()
                        {
                            Username = SignUpLogin,
                            Password = SignUpPassword,
                            ConfirmPassword = SignUpPasswordConfirm
                        };

                        App.UsersDatabase.SaveItem(newUser);

                        Acr.UserDialogs.UserDialogs.Instance.Alert("Registration successful");

                        var parameter = new NavigationParameters();
                        parameter.Add("LoginParameter", SignUpLogin);
                        await _navigationService.NavigateAsync(new Uri($"SignInPage?LoginParameter={SignUpLogin}", UriKind.Relative));

                    }

                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Password doesn't match the congirmation");
                    }
                }
            }

        }

        #endregion
    }
}
