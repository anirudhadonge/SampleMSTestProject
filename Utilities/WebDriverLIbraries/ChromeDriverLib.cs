using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMSTestProject.Utilities.WebDriverLIbraries
{
    public class ChromeDriverLib
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IWebDriver GetChromeDriver()
        {
            IWebDriver driver;
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("test-type");
            chromeOptions.AddArgument("--allow-running-insecure-content");
            chromeOptions.AddArgument("--disable-extensions");
            chromeOptions.AddArgument("--ignore-certificate-errors");
            chromeOptions.AddArgument("--no-sanbox");
            chromeOptions.AddArgument("--start-maximized");
            driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOptions, TimeSpan.FromMinutes(3));
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
