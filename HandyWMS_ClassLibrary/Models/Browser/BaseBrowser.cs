using HandyWMS_ClassLibrary.Enum.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWMS_ClassLibrary.Models.Browser
{
    public class BaseBrowser
    {
        public string Name { get; set; }
        public string Maker { get; set; }
        public BrowserType Type { get; set; } = BrowserType.Generic;
        public Version Version { get; set; }

        public BaseBrowser() { }
        public BaseBrowser(BrowserType browserType) => Type = browserType;
        public BaseBrowser(BrowserType browserType, Version version) : this(browserType) => Version = version;

        public BaseBrowser(string name)
        {
            //BrowserType type = null;

            ////if (!System.Enum.TryParse(name, true, out type))
            ////    throw new BrowserNotFoundException(name, "not found");

            //Type = type;
        }
        public Version ToVersion(string version)
        {
            version = RemoveWhitespace(version);
            return Version.TryParse(version, out var parsedVersion) ? parsedVersion : new Version(0, 0);
        }
        public string RemoveWhitespace(string version) => version.Contains(" ") ? version.Replace(" ", "") : version;
    }
}
