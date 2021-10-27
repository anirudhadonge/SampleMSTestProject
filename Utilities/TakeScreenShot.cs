using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SampleMSTestProject.Utilities
{
    public class TakeScreenShot
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string Capture(IWebDriver driver)
        {
            try
            {
                log.Info("Create the Screen Shot");
                ITakesScreenshot takeScreenShot = (ITakesScreenshot)driver;
                Screenshot screenshot =takeScreenShot.GetScreenshot();
                string workingDirectory = CreatedScreenshotDirectory();
                string screenShotName = "ScreenShot_" + DateTime.Now.ToFileTime();
                string finalPath =workingDirectory+"\\"+screenShotName+."png";
                string localPath = new Uri(finalPath).LocalPath;
                screenshot.SaveAsFile(finalPath);
                log.Info("ScreenShot Path:" + localPath);
                return localPath;

            }catch(Exception ex)
            {
                log.Error(ex.ToString());
                return string.Empty;
            }
        }

        private static string CreatedScreenshotDirectory()
        {
            try
            {
                string screenShotDirectory = Directory.GetCurrentDirectory() + "\\ExtentReport\\ScreenShots";
                if (!Directory.Exists(screenShotDirectory))
                {
                    Directory.CreateDirectory(screenShotDirectory);
                }
                log.Info(screenShotDirectory);
                return screenShotDirectory;
            }catch(Exception ex)
            {
                log.Error(ex.ToString());
            }
            return null;
        }

        public static string CaptureBase64(IWebDriver driver)
        {
            try
            {
                log.Info("Create the screen shot with ITakeScreenShot");
                ITakesScreenshot takeScreenShot = (ITakesScreenshot)driver;
                Screenshot screenshot = takeScreenShot.GetScreenshot();
                return screenshot.AsBase64EncodedString;
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
            return null;
        }
    }
}
