using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SampleMSTestProject.Utilities
{
    public class ReflectionLibrarycs
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static object GetRunTimeObject(string className, string billerName, object testContext)
        {
            string classPackageName = "BillMatrixAutomation.PageModel."+billerName+"."+className;
            return GetDynamicObject(testContext, classPackageName);
        }

        public static object GetDynamicObject(object testContext, string classNameSpace)
        {
            try
            {
                log.Info("The Class that is getting initialized is " + classNameSpace);
                Assembly executing = Assembly.GetExecutingAssembly();
                return Activator.CreateInstance(executing.GetType(classNameSpace), testContext);
            }catch(Exception ex)
            {
                log.Info(ex.ToString());
            }
            return null;
        }
    }
}
