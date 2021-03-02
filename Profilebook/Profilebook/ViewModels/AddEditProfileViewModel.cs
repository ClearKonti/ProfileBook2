using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Profilebook.ViewModels
{
    public class AddEditProfileViewModel : BindableBase
    {
        public AddEditProfileViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #region --- Services ---

        INavigationService _navigationService;

        #endregion

        #region --- Public Properties ---

        public ICommand SaveToolBarItemCommand => new Command(SaveToolBarItem);


        private string _profileNick;
        public string ProfileNick
        {
            get => _profileNick;
            set => SetProperty(ref _profileNick, value);
        }

        private string _profileName;
        public string ProfileName
        {
            get => _profileName;
            set => SetProperty(ref _profileName, value);
        }

        private string _profileDescription;
        public string ProfileDescription
        {
            get => _profileDescription;
            set => SetProperty(ref _profileDescription, value);
        }

        #endregion

        #region --- Private Helpers ---

        private async void SaveToolBarItem(object obj)
        {
            var parameter = new NavigationParameters();
            parameter.Add("Nick", $"{ProfileNick}");
            parameter.Add("Name", ProfileName);
            parameter.Add("Description", ProfileDescription);

            await _navigationService.NavigateAsync(new Uri("MainList", UriKind.Relative), parameter);
            
        }

        #endregion
    }
}
