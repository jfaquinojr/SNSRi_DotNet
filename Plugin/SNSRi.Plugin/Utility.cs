using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SNSRi.Plugin
{
    public static class Utility
    {
        public static string GetConfigValue(string appSetting, string defaultValue)
        {
            var value = ConfigurationManager.AppSettings[appSetting];
            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }
            return value;
        }
    }
}
