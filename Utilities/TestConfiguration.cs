using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;

namespace SampleMSTestProject.Utilities
{
    class TestConfiguration
    {
        private IConfiguration config;

        public TestConfiguration()
        {
            config = GetConfigurationRoot();
        }


        public IConfiguration GetConfigurationRoot()
        {
            string directory = Directory.GetCurrentDirectory();
            return new ConfigurationBuilder().SetBasePath(directory).AddJsonFile("AppSsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public string GetConfigurationValue(string configKey)
        {
            return config.GetSection("configSetting").GetSection(configKey).Value;
        }

        public static IConfigurationRoot GetConfigurationRoot(string folderName, string jsonFileName)
        {
            string homeDir = Directory.GetCurrentDirectory() + "\\" + folderName;
            return new ConfigurationBuilder().SetBasePath(homeDir).AddJsonFile("AppSsettings.json", optional: true, reloadOnChange: true)
            .Build();
        }
    }
}
