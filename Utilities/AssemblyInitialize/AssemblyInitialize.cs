using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

[assembly:log4net.Config.XmlConfigurator(Watch = true)]
namespace SampleMSTestProject.Utilities.AssemblyInitialize
{
    [TestClass]
    public class AssemblyInitialize
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [AssemblyInitialize]
        public static void InitializeAssembly(TestContext testContext)
        {
            log.Info("------------------Test execution started------------------");
            ExtentReporting.GetExtentReports();
        }


        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            ExtentReporting.GetExtentReports().Flush();
        }

    }
}
