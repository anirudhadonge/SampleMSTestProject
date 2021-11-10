using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleMSTestProject.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMSTestProject.PageModel.BasePageModel
{
    public abstract class BaseModel : BaseLoggerLib
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BaseModel(BaseTestContext baseTextContext) :base(baseTextContext)
        {
            
        }

    }
}
