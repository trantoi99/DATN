using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Electric
{
    public static class AppSetting
    {
        public static AppSettingConfig Configuration
        {
            get
            {
                using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "appsetting.json")))
                {
                    AppSettingConfig appSetting = JsonConvert.DeserializeObject<AppSettingConfig>(reader.ReadToEnd());
                    return appSetting;
                }
            }
        }
    }

    public class AppSettingConfig
    {
        public Email EmailConfig { get; set; }

        public class Email
        {
            public string ApiKey { get; set; }
            public string ApiSecret { get; set; }
            public string NameIdentifier { get; set; }
            public string EmailIdentifier { get; set; }
        }
    }
}