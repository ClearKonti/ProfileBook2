using Prism.Mvvm;
using Prism.Navigation;
using Profilebook.Services.Profile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Profilebook.ViewModels
{
    public class AddEditProfileViewModel : BindableBase
    {
        
        public AddEditProfileViewModel(INavigationService navigationService, IProfileService profileService)
        {
            _navigationService = navigationService;
            _profileService = profileService;
        }

        #region --- Services ---

        INavigationService _navigationService;
        IProfileService _profileService;

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

        #region --- Overrides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(ProfileNick))
            {
                _profileService.ProfileNick = ProfileNick;
            }

            if (args.PropertyName == nameof(ProfileName))
            {
                _profileService.ProfileName = ProfileName;
            }

            if (args.PropertyName == nameof(ProfileDescription))
            {
                _profileService.ProfileDescription = ProfileDescription;
            }
        }

        #endregion

        #region --- Private Helpers ---

        private async void SaveToolBarItem(object obj)
        {
            
            _profileService.ProfileNick = ProfileNick;
            _profileService.ProfileName = ProfileName;
            _profileService.ProfileDescription = ProfileDescription;


            await _navigationService.GoBackAsync();

        }

        #endregion
    }
}
