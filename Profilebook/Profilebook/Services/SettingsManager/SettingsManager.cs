using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Profilebook.Services.SettingsManager
{
    public class SettingsManager : ISettingsManager
    {
        public bool IsAuthorised 
        {
            get => Preferences.Get(nameof(IsAuthorised), false);
            set => Preferences.Set(nameof(IsAuthorised), value);
        }
    }
}
