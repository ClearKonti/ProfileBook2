using Prism.Mvvm;
using Prism.Navigation;
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
    public class MainListViewModel : BindableBase, INavigatedAware, IInitializeAsync
    {

        public MainListViewModel(ISettingsManager settingsManager, INavigationService navigationService, IProfileService profileService, IRepository repository)
        {
            _navigationService = navigationService;
            _settingsManager = settingsManager;
            _profileService = profileService;

            _repository = repository;

            IsAuthorised = _settingsManager.IsAuthorised;

            //ProfileList = new ObservableCollection<ProfileModel>()
            //{
            //     new ProfileModel()
            //     {
            //         FirstName = "Vasya",
            //         LastName = "Vasyaaa",
            //         CreationDate = DateTime.Now,
            //         Description = "My mate"
            //     },

            //     new ProfileModel()
            //     {
            //         FirstName = "Lera",
            //         LastName = "Bri",
            //         CreationDate = DateTime.Now,
            //         Description = "Me"
            //     }
            //};
        }

        #region --- Services --- 

        INavigationService _navigationService;
        private ISettingsManager _settingsManager;
        IProfileService _profileService;
        private IRepository _repository;

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

            if (_profileService.ProfileNick != null)
            {
                AddProfileProcess();
            }
        }

        #endregion

        #region --- Public properties ---

        public ICommand AddProfileCommand => new Command(AddProfile);
        public ICommand ExitToolBarCommand => new Command(ExitToolBarItem);
        public ICommand SettingsToolBarCommand => new Command(SettingsToolBarItem);
        public ICommand OnEditTapCommand => new Command(OnEditTap);
        public ICommand OnDeleteTapCommand => new Command(OnDeleteTap);
        
        public async void OnDeleteTap()
        {

            ProfileList.Remove(SelectedItem);
            await _repository.DeleteAsync(SelectedItem);
        }

        public async void OnEditTap()
        {
            await _repository.DeleteAsync(SelectedItem);

            ProfileList.Remove(SelectedItem);
        }

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

        private bool _isAuthorised;
        public bool IsAuthorised
        {
            get => _isAuthorised;
            set => SetProperty(ref _isAuthorised, value);
        }

        private ProfileModel _selectedItem;
        public ProfileModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public ObservableCollection<ProfileModel> _profileList;
        public ObservableCollection<ProfileModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
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
            
            //profile.Id = id;

            ProfileList.Add(profile);


            _profileService.ClearProfileValues();
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


        public async Task InitializeAsync(INavigationParameters parameters)
        {

            var profileList = await _repository.GetAllAsync<ProfileModel>();

            ProfileList = new ObservableCollection<ProfileModel>(profileList);
        }

        #endregion
    }
}
