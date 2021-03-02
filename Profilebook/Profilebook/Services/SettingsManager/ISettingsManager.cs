using System;
using System.Collections.Generic;
using System.Text;

namespace Profilebook.Services.SettingsManager
{
    public interface ISettingsManager
    {
        bool IsAuthorised { get; set; }

    }
}
