using System;
using System.Collections.Generic;
using System.Text;
using Profilebook.Model;
using Xamarin.Essentials;

namespace Profilebook.Services.Profile
{
    public class ProfileService : IProfileService
    {
        public string ProfileNick 
        {
            get => Preferences.Get(nameof(ProfileNick), null);
            set => Preferences.Set(nameof(ProfileNick), value);
        }
        public string ProfileName
        {
            get => Preferences.Get(nameof(ProfileName), null);
            set => Preferences.Set(nameof(ProfileName), value);
        }
        public string ProfileDescription
        {
            get => Preferences.Get(nameof(ProfileDescription), null);
            set => Preferences.Set(nameof(ProfileDescription), value);
        }

        public void ClearProfileValues()
        {
            ProfileNick = null;
            ProfileName = null;
            ProfileDescription = null;
        }
    }
}
