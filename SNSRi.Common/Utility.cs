using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SNSRi.Common
{
    public static class Utility
    {
        public static string GetConfig(string settingName, string defaultValue)
        {
            var value = ConfigurationManager.AppSettings[settingName];
            if (string.IsNullOrWhiteSpace(value))
            {
                value = defaultValue;
            }
            return value;
        }
    }
}