using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMSTestProject.Utilities
{
    public class DataBaseConnection
    {
        private IConfiguration iconfig;

        private string environment;

        public DataBaseConnection()
        {
            environment = new TestConfiguration().GetConfigurationValue("environment");
            iconfig = TestConfiguration.GetConfigurationRoot("Database", "Database");
        }

        public string GetConfigurationValue(string configKey)
        {
            return iconfig.GetSection(environment).GetSection(configKey).Value;
        }
    }
}
