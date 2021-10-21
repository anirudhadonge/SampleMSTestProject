using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using SampleMSTestProject.Utilities.WebDriverLIbraries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMSTestProject.Utilities
{
    public class WebDriverLibrary
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string browser;

        public WebDriverLibrary()
        {
            browser = "Chrome";
        }

        public IWebDriver GetWebDriver()
        {
            log.Info("The WebDriver for " + browser);
            switch (browser.ToLower())
            {
                case "chrome":
                    return GetEventFiringListener(new ChromeDriverLib().GetChromeDriver());
                    break;
                Default:
                    log.Info("No browser specified");
            }
            return null;
        }

        public IWebDriver GetEventFiringListener(IWebDriver driver)
        {
            EventFiringWebDriver eventFiringWebDriver = new EventFiringWebDriver(driver);
            return eventFiringWebDriver;
        }
    }
}
