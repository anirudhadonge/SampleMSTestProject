using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SampleMSTestProject.Utilities
{
    public class ExtentReporting
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static ExtentReports extentReport = null;

        private ExtentReporting()
        {

        }

        public static string CreateReportDirectory()
        {
            var path = Directory.GetCurrentDirectory() + "\\ExtentReports";
            log.Info("The Extent Report directory " + path);
            if (!Directory.Exists(path))
            {
                log.Info("Creating the directory at " + path);
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public static ExtentReports GetExtentReports()
        {
            log.Info("Check for Extent Report Object");
            if(extentReport == null)
            {
                log.Info("Creating the Extent Report Object");
                string reportFileName = DateTime.Now.ToString("dd_MMMM_yyyy_HH_mm_ss");
                var path = CreateReportDirectory();
                var htmlReporter = new ExtentV3HtmlReporter(path+"/ExtentReport_"+reportFileName+".html");
                htmlReporter.Start();
                htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
                extentReport.AttachReporter(htmlReporter);
            }
            return extentReport;
        }

        public static void FlushExtentObject()
        {
            log.Info("Flush the extent Report Object");
            extentReport.Flush();
        }
    }
}
