using Acr.UserDialogs;
using Prism.Ioc;
using Prism.Unity;
using Profilebook.Services.Repository;
using Profilebook.Services.SettingsManager;
using Profilebook.Tables;
using Profilebook.ViewModels;
using Profilebook.Views;
using System;
using System.IO;
using Xamarin.Forms;

namespace Profilebook
{
    public partial class App : PrismApplication
    {
        public App()
        {
        }

        //private ISettingsManager _settingsManager;

        public static UsersRepository usersDatabase;
        public static UsersRepository UsersDatabase
        {
            get
            {
                if (usersDatabase == null)
                {
                    usersDatabase = new UsersRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "users.db"));
                }
                return usersDatabase;
            }
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<MainList, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfile, AddEditProfileViewModel>();
        }
       
        protected override async void OnInitialized()
        {
            InitializeComponent();

            //if(_settingsManager.IsAuthorised == true)
            // {
            //     await NavigationService.NavigateAsync(new System.Uri("http://www.Profilebook/NavigationPage/MainList", System.UriKind.Absolute));
            // }

            await NavigationService.NavigateAsync(new System.Uri("http://www.Profilebook/SignInPage", System.UriKind.Absolute));
            //await NavigationService.NavigateAsync(new System.Uri("http://www.Profilebook/NavigationPage/MainList", System.UriKind.Absolute));

        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
