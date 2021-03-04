using System;
using System.Collections.Generic;
using System.Text;

namespace Profilebook.Services.Profile
{
    public interface IProfileService
    {
        string ProfileNick { get; set; }
        string ProfileName { get; set; }
        string ProfileDescription { get; set; }
        void ClearProfileValues();
    }
}
