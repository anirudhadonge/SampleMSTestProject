using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMSTestProject.Utilities
{
    public class BaseLoggerLib
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BaseLoggerLib(BaseTestContext baseTextContext)
        {
            BaseTestContext = baseTextContext; 
        }

        public BaseTestContext BaseTestContext { get; set; }

        public virtual void LogInfo(ILog log, string message)
        {
            BaseTestContext.TestContext.WriteLine(message);
            log.Info(message);
        }

        public virtual void LogError(ILog log, string message)
        {
            BaseTestContext.TestContext.WriteLine(message);
            log.Error(message);
        }

        public virtual void LogInfoMessage(ILog log, string message)
        {
            BaseTestContext.TestContext.WriteLine(message);
            log.Info(message);
            BaseTestContext.ExtentTest.Info(message);
        }

        public virtual void LogErrorMessage(ILog log, string message)
        {
            BaseTestContext.TestContext.WriteLine(message);
            log.Error(message);
            BaseTestContext.ExtentTest.Error(message);
        }

        public virtual void LogInfoMessage(ILog log, string message, string base64String)
        {
            BaseTestContext.TestContext.WriteLine(message);
            log.Info(message);
            BaseTestContext.ExtentTest.Info(message, GetMediaEntityBuilder(base64String));
        }

        public virtual void LogFailureMessage(ILog log, string message)
        {
            BaseTestContext.TestContext.WriteLine(message);
            BaseTestContext.TestContext.AddResultFile(TakeScreenShot.Capture(BaseTestContext.Driver));
            log.Error(message);
            BaseTestContext.ExtentTest.Fail(message, GetMediaEntityBuilder(TakeScreenShot.CaptureBase64(BaseTestContext.Driver)));
            Assert.Fail(message);
        }

        public virtual void LogFailureMessageWoScreenshot(ILog log, string message)
        {
            BaseTestContext.TestContext.WriteLine(message);
            log.Error(message);
            BaseTestContext.ExtentTest.Fail(message);
            Assert.Fail(message);
        }

        public virtual void LogFailure(ILog log, string message)
        {
            BaseTestContext.TestContext.WriteLine(message);
            log.Error(message);
            BaseTestContext.ExtentTest.Fail(message);
        }

        public virtual void LogSuccessMessage(ILog log, string message)
        {
            BaseTestContext.TestContext.WriteLine(message);
            log.Info(message);
            BaseTestContext.ExtentTest.Pass(message);
        }

        public virtual void LogInfoXMLBlock(ILog log, string message, string xmlString)
        {
            log.Info(message + xmlString);
            BaseTestContext.ExtentTest.Info(message);
            BaseTestContext.ExtentTest.Info(MarkupHelper.CreateCodeBlock(xmlString, CodeLanguage.Xml));
            BaseTestContext.TestContext.WriteLine(message + xmlString);
        }

        public virtual void LogPassXMLBlock(ILog log, string message, string xmlString)
        {
            log.Info(message + xmlString);
            BaseTestContext.ExtentTest.Pass(message);
            BaseTestContext.ExtentTest.Pass(MarkupHelper.CreateCodeBlock(xmlString, CodeLanguage.Xml));
            BaseTestContext.TestContext.WriteLine(message + xmlString);
        }

        public virtual void LogFailXMLBlock(ILog log, string message, string xmlString)
        {
            log.Info(message + xmlString);
            BaseTestContext.ExtentTest.Fail(message);
            BaseTestContext.ExtentTest.Fail(MarkupHelper.CreateCodeBlock(xmlString, CodeLanguage.Xml));
            BaseTestContext.TestContext.WriteLine(message + xmlString);
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
    }
}
