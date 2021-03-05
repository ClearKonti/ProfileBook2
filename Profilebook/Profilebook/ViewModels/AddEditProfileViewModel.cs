using Prism.Mvvm;
using Prism.Navigation;
using Profilebook.Enums;
using Profilebook.Model;
using Profilebook.Services.Profile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Profilebook.ViewModels
{
    public class AddEditProfileViewModel : BindableBase, INavigatedAware
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


        private ProfileModel _profileModel;
        public ProfileModel ProfileModel
        {
            get => _profileModel;
            set => SetProperty(ref _profileModel, value);
        }
        #endregion

        #region --- Public Methods ---
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ProfileModel profileModel = (ProfileModel)parameters[$"{nameof(ProfileModel)}"];

            if (profileModel != null)
            {
                _profileService.ProfilePageMode = (int)AddEditModes.Edit;
                ProfileNick = profileModel.FirstName;
                ProfileName = profileModel.LastName;
                ProfileDescription = profileModel.Description;
            }

            else _profileService.ProfilePageMode = (int)AddEditModes.Add;
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
