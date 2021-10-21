using AventStack.ExtentReports;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMSTestProject.Utilities
{
    public class BaseTestContext
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BaseTestContext(IWebDriver driver , ExtentTest extentTest, TestContext testContext)
        {
            this.Driver = driver;
            this.ExtentTest = extentTest;
            this.TestContext = testContext;
        }


        public BaseTestContext(ExtentTest extentTest, TestContext testContext)
        {
               this.ExtentTest = extentTest;
            this.TestContext = testContext;
        }

        public IWebDriver Driver { get; set; }

        public ExtentTest ExtentTest { get; set; }

        public TestContext TestContext { get; set; }


    }
}
