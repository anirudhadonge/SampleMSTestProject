using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMSTestProject.Utilities.BaseTestLibrary
{
    public class BaseUITest : BaseTest
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public BaseUITest()
        {

        }

        [TestInitialize]
        public void BaseUiTestInitialize()
        {
            baseTestContext = new BaseTestContext(new WebDriverLibrary().GetWebDriver(), extentTest, TestContext);
        }
    }
}
