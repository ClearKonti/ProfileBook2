using Prism.Mvvm;
using Prism.Navigation;
using Profilebook.Model;
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
    public class MainListViewModel : BindableBase, INavigatedAware
    {

        public MainListViewModel(ISettingsManager settingsManager, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _settingsManager = settingsManager;


            IsAuthorised = _settingsManager.IsAuthorised;

            ProfileList = new ObservableCollection<ProfileModel>()
            {
                 new ProfileModel()
                 {
                     FirstName = "Vasya",
                     LastName = "Vasyaaa",
                     CreationDate = DateTime.Now,
                     Description = "My mate"
                 },

                 new ProfileModel()
                 {
                     FirstName = "Lera",
                     LastName = "Bri",
                     CreationDate = DateTime.Now,
                     Description = "Me"
                 }
            };
        }

        #region --- Services --- 

        INavigationService _navigationService;
        private ISettingsManager _settingsManager;

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            string nick = (string)parameters["Nick"];
            string name = (string)parameters["Name"];
            string description = (string)parameters["Description"];
            AddProfileProcess(nick, name, description);
        }

        #endregion

        #region --- Public properties ---

        public ICommand AddProfileCommand => new Command(AddProfile);
        public ICommand ExitToolBarCommand => new Command(ExitToolBarItem);
        public ICommand SettingsToolBarCommand => new Command(SettingsToolBarItem);

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

        public ObservableCollection<ProfileModel> _contactList;
        public ObservableCollection<ProfileModel> ProfileList
        {
            get => _contactList;
            set => SetProperty(ref _contactList, value);
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
        private async void AddProfile (object obj)
        {
            
            await _navigationService.NavigateAsync("AddEditProfile");
        }

        private void AddProfileProcess(string nickName, string name, string description)
        {
            var profile = new ProfileModel()
            {
                FirstName = nickName,
                LastName = name,
                Description = description,
                CreationDate = DateTime.Now

            };
            ProfileList.Add(profile);

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
