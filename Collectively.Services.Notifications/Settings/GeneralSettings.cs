using System.Collections.Generic;

namespace Collectively.Services.Notifications.Settings
{
    public class GeneralSettings
    {
        public string RemarksPath { get; set; }
        public string DefaultCulture { get; set; }
        public IEnumerable<string> SupportedCultures { get; set; }
    }
}