using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMSTestProject.Utilities.BaseTestLibrary
{
    public class BaseAPITest : BaseTest
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public BaseAPITest()
        {

        }

        [TestInitialize]
        public void BaseAPITestInitialize()
        {
            baseTestContext = new BaseTestContext(extentTest, TestContext);
        }
    }
}
