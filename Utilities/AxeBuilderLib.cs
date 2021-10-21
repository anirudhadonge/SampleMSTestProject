using AventStack.ExtentReports;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Selenium.Axe;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMSTestProject.Utilities
{
    public class AxeBuilderLib
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private AxeBuilder axeBuilder;

        private AxeResult axeResult;

        private ExtentTest extentTest;

        public AxeBuilderLib(IWebDriver driver, ExtentTest extentTest)
        {
            log.Info("Initialize the AxeBuilder Library");
            axeBuilder = new AxeBuilder(driver);
            this.extentTest = extentTest;
        }


        public void AnalyisePage(string tags)
        {
            string[] tagArray = tags.Split(',');
            log.Info("Create the Axe Result Object with Axe Tags " + tagArray.ToString());
            axeResult = axeBuilder.WithTags(tagArray).Analyze();
        }

        public AxeResult GetAxeResult()
        {
            return axeResult;
        }

        public AxeResultItem[] GetViolations()
        {
            return axeResult.Violations;
        }

        public void ValidateViolations()
        {
            log.Info("Verify the Violations in the Page");
            Assert.IsTrue(axeResult.Violations.Length == 0);
        }

        public void AnalysePageForViolations(string tags)
        {
            AnalyisePage(tags);
            ValidateViolations();
            log.Info("Violation count is :" + GetViolations().Length);
        }
    }
    
}
