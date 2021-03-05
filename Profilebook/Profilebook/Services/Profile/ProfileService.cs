using System;
using System.Collections.Generic;
using System.Text;
using Profilebook.Model;
using Xamarin.Essentials;

namespace Profilebook.Services.Profile
{
    public class ProfileService : IProfileService
    {

        public string ProfileNick { get; set; }

        public string ProfileName { get; set; }

        public string ProfileDescription { get; set; }

        public int ProfilePageMode { get; set; }

        public void ClearProfileValues()
        {
            ProfileNick = null;
            ProfileName = null;
            ProfileDescription = null;
        }
    }
}
