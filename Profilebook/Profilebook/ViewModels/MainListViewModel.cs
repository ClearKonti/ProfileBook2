using Prism.Mvvm;
using Prism.Navigation;
using Profilebook.Enums;
using Profilebook.Model;
using Profilebook.Services.Profile;
using Profilebook.Services.Repository;
using Profilebook.Services.SettingsManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Profilebook.ViewModels
{
    public class MainListViewModel : 
        BindableBase, 
        INavigatedAware, 
        IInitializeAsync
    {

        public MainListViewModel(ISettingsManager settingsManager, INavigationService navigationService, IProfileService profileService, IRepository repository)
        {
            _navigationService = navigationService;
            _settingsManager = settingsManager;
            _profileService = profileService;

            _repository = repository;

            IsAuthorised = _settingsManager.IsAuthorised;
        }

        #region --- Services --- 

        INavigationService _navigationService;
        ISettingsManager _settingsManager;
        IProfileService _profileService;
        IRepository _repository;

        
        #endregion

        #region --- Public properties ---

        public ICommand AddProfileCommand => new Command(AddProfile);
        public ICommand ExitToolBarCommand => new Command(ExitToolBarItem);
        public ICommand SettingsToolBarCommand => new Command(SettingsToolBarItem);
        public ICommand OnEditTapCommand => new Command(OnEditTap);
        public ICommand OnDeleteTapCommand => new Command(OnDeleteTap);

        
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);


        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private ProfileModel _editedProfile;
        public ProfileModel EditedProfile
        {
            get => _editedProfile;
            set => SetProperty(ref _editedProfile, value);
        }

        private bool _isAuthorised;
        public bool IsAuthorised
        {
            get => _isAuthorised;
            set => SetProperty(ref _isAuthorised, value);
        }

        //private ProfileModel _selectedItem;
        //public ProfileModel SelectedItem
        //{
        //    get => _selectedItem;
        //    set => SetProperty(ref _selectedItem, value);
        //}
        
        

        public ObservableCollection<ProfileModel> _profileList;
        public ObservableCollection<ProfileModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }


        #endregion

        #region --- Public Methods ---

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (_profileService.ProfilePageMode == (int)AddEditModes.Edit && _profileService.ProfileNick != null)
            {
                ProfileList.Remove(EditedProfile);
                await _repository.DeleteAsync(EditedProfile);

                AddProfileProcess();

            }
            if (_profileService.ProfilePageMode == (int)AddEditModes.Add && _profileService.ProfileNick != null)
            {
                AddProfileProcess();
            }
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {

            var profileList = await _repository.GetAllAsync<ProfileModel>();

            ProfileList = new ObservableCollection<ProfileModel>(profileList);
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
        private async void AddProfile ()
        {
            await _navigationService.NavigateAsync("AddEditProfile");
        }

        private async void AddProfileProcess()
        {


            var profile = new ProfileModel()
            {
                FirstName = _profileService.ProfileNick,
                LastName = _profileService.ProfileName,
                Description = _profileService.ProfileDescription,
                CreationDate = DateTime.Now

            };
            await _repository.InsertAsync(profile);

            ProfileList.Add(profile);

            _profileService.ClearProfileValues();
        }

        private async void OnDeleteTap(object obj)
        {
            if (obj != null)
            {
                ProfileList.Remove((ProfileModel)obj);
                await _repository.DeleteAsync((ProfileModel)obj);
            }
        }
        private async void OnEditTap(object obj)
        {
            var parameter = new NavigationParameters();
            parameter.Add(nameof(ProfileModel), (ProfileModel)obj);
            EditedProfile = (ProfileModel)obj;

            await _navigationService.NavigateAsync($"AddEditProfile", parameter);
        }

         private async void ExitToolBarItem(object obj)
        {
            IsAuthorised = false;
            await _navigationService.NavigateAsync(new System.Uri("http://www.Profilebook/SignInPage", System.UriKind.Absolute));

        }

        private void SettingsToolBarItem(object obj)
        {
            Acr.UserDialogs.UserDialogs.Instance.Alert("Settings");
        }

        #endregion
    }
}
