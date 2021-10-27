using AventStack.ExtentReports;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SampleMSTestProject.Utilities.CustomerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleMSTestProject.Utilities.BaseTestLibrary
{
    public class BaseTest
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected static int PassTestCaseCount = 0;
        protected static int FailedTestCaseCount = 0;
        protected ExtentTest extentTest;
        protected BaseTestContext baseTestContext;
        protected BaseCustomerData customerData;

        public BaseTest()
        {

        }

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void BaseTestInitialize()
        {
            try
            {
                log4net.ThreadContext.Properties["Guid"] = Guid.NewGuid().ToString();
                extentTest = ExtentReporting.GetExtentReports().CreateTest(TestContext.TestName, GetTestDescription()).AssignCategory(GetTestCategories());
                LogInfoMessage(log, $"************************************* Base Class Initialzed block starts *************************");
                customerData = UtilityLibrary.GetCustomerData(TestContext.TestName);

            }
            catch (Exception ex)
            {
                LogErrorMessage(log, ex.ToString());
            }
            LogInfoMessage(log, $"************************************* Base Class Initialzed block ended *************************");
        }

        public virtual void LogInfo(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Info(message);
        }

        public virtual void LogError(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Error(message);
        }

        public virtual void LogInfoMessage(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Info(message);
            extentTest.Info(message);
        }

        public virtual void LogErrorMessage(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Error(message);
            extentTest.Error(message);
        }

        public virtual void LogInfoMessage(ILog log, string message, string base64String)
        {
            TestContext.WriteLine(message);
            log.Info(message);
            extentTest.Info(message, GetMediaEntityBuilder(base64String));
        }

        public virtual void LogFailureMessage(ILog log, string message)
        {
            TestContext.WriteLine(message);
            TestContext.AddResultFile(TakeScreenShot.Capture(baseTestContext.Driver));
            log.Error(message);
            extentTest.Fail(message, GetMediaEntityBuilder(TakeScreenShot.CaptureBase64(baseTestContext.Driver)));
            Assert.Fail(message);
        }

        public virtual void LogFailureMessageWoScreenshot(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Error(message);
            extentTest.Fail(message);
            Assert.Fail(message);
        }

        public virtual void LogFailure(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Error(message);
            extentTest.Fail(message);
        }

        public virtual void LogSuccessMessage(ILog log, string message)
        {
            TestContext.WriteLine(message);
            log.Info(message);
            extentTest.Pass(message);
        }

        public virtual void DisposeDriver()
        {
            try
            {
                LogInfoMessage(log, "Disposing the WebDriver Object", TakeScreenShot.CaptureBase64(baseTestContext.Driver));
                LogTestOutCome();
                log.Info("Driver is getting closed");
                baseTestContext.Driver.Close();
                log.Info("Driver is closed");
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
            finally
            {
                baseTestContext.Driver.Quit();
            }
        }

        public MediaEntityModelProvider GetMediaEntityBuilder(string base64String)
        {
            try
            {
                return MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64String).Build();
            }
            catch (Exception ex)
            {
                log.Info(ex.ToString());
                return null;
            }
        }

        public void LogTestOutCome()
        {
            string message = TestContext.TestName + " is " + TestContext.CurrentTestOutcome.ToString();
            switch (TestContext.CurrentTestOutcome.ToString())
            {
                case "Passed":
                    LogSuccessMessage(log, message);
                    break;
                case "Inconclusive":
                    LogInfoMessage(log, message);
                    break;
                default:
                    LogFailure(log, message);
                    break;
            }
        }

        [TestCleanup]
        public void BaseTestCleanUp()
        {
            try
            {
                LogInfoMessage(log, "************************************* base class cleanup block started *************************");
                if (TestContext.CurrentTestOutcome == UnitTestOutcome.Passed)
                {
                    PassTestCaseCount++;
                }
                else
                {
                    FailedTestCaseCount++;
                }
                PrintExecutionStatus();
                ExtentReporting.FlushExtentObject();
            }
            catch (Exception ex)
            {
                LogErrorMessage(log, "Base Cleanup Exception :" + ex);
            }
            finally
            {
                LogInfoMessage(log, "************************************* base class cleanup block Ends *************************");
            }
        }

        protected void PrintExecutionStatus()
        {
            LogInfoMessage(log, "******************** Current Execution Outcome **************************");
            LogInfoMessage(log, "*************************************************************************");
            LogInfoMessage(log, "Total Tests Executed: " + PassTestCaseCount + FailedTestCaseCount);
            LogInfoMessage(log, "Total Tests Passed: " + PassTestCaseCount);
            LogInfoMessage(log, "Total Tests Failed: " + FailedTestCaseCount);
            LogInfoMessage(log, "*************************************************************************");
        }

        public string GetTestDescription()
        {
            var currentClassType = this.GetType().Assembly.GetTypes().FirstOrDefault(f => f.FullName == TestContext.FullyQualifiedTestClassName);
            var currentMethod = currentClassType.GetMethod(TestContext.TestName);
            string description = ((DescriptionAttribute[])currentMethod.GetCustomAttributes(typeof(DescriptionAttribute), true))[0].Description;
            return string.IsNullOrEmpty(description) ? TestContext.TestName : description;
        }

        private string[] GetTestCategories()
        {
            var currentClassType = this.GetType().Assembly.GetTypes().FirstOrDefault(f => f.FullName == TestContext.FullyQualifiedTestClassName);
            var currentMethod = currentClassType.GetMethod(TestContext.TestName);
            var requiredCat = (from attribute in (IEnumerable<TestCategoryAttribute>)currentMethod.GetCustomAttributes(typeof(TestCategoryAttribute), true)
                               from category in attribute.TestCategories
                               where !category.Contains("Smoke") && !category.Contains("Smoke")
                               select category).ToArray();

            return requiredCat.Length > 0 ? requiredCat : new string[] { "Unassigned" };

        }
    }

}

