using System.Configuration;
using PropertyList.BusinessLogic.Constant;

namespace PropertyList.Helper
{
    public class ApplicationUriResolver : IApplicationUriResolver
    {
        public string GetBaseUrl()
        {
            return ConfigurationManager.AppSettings[ConfigKeys.ApplicationUri];
        }
    }
}