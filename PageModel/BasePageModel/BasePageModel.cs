using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SampleMSTestProject.Utilities;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMSTestProject.PageModel.BasePageModel
{
    public abstract class BasePageModel : BaseModel
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IWebDriver driver;

        private WebDriverWait wait;

        public BasePageModel(BaseTestContext baseTextContext) : base(baseTextContext)
        {
            this.wait = new WebDriverWait(BaseTestContext.Driver, TimeSpan.FromSeconds(30));
        }

        public bool IsElementPresentAndVisible(By objectIdentifier, log4net.ILog log)
        {
            try
            {
                IWebElement webElement = BaseTestContext.Driver.FindElement(objectIdentifier);
                if (webElement != null && webElement.Displayed)
                {
                    LogInfo(log, " Element found using the identifier :" + objectIdentifier);
                    return true;
                }
            }
            catch (Exception exception)
            {
                LogError(log, " Element found using the identifier :" + objectIdentifier + exception.ToString());
            }
            return false;
        }

        public void CLickOnElement(IWebElement element)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
            element.Click();
        }

        public void ClickOnElementWithJS(IWebDriver element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)BaseTestContext.Driver;
            js.ExecuteScript("arguments[0].click();", element);
        }

        public void ScrollIntoView(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", element);
        }

        public void UnCheckELement(By element)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
            IWebElement checkBox = BaseTestContext.Driver.FindElement(element);

            if (checkBox.Selected)
            {
                checkBox.Click();
            }
        }

        public void CheckELement(By element)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
            IWebElement checkBox = BaseTestContext.Driver.FindElement(element);

            if (!checkBox.Selected)
            {
                checkBox.Click();
            }
        }


        public void ClickElementByAction(By element)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
            IWebElement iElement = BaseTestContext.Driver.FindElement(element);
            Actions actions = new Actions(driver);
            actions.MoveToElement(iElement).Click().Build().Perform();

        }

        public void EnterText(By element, string text)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
            IWebElement webElement = BaseTestContext.Driver.FindElement(element);
            webElement.Clear();
            webElement.SendKeys(text);
        }

        public void EnterTextBySelection(By element, string text)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
            IWebElement webElement = BaseTestContext.Driver.FindElement(element);
            webElement.SendKeys(Keys.Control + "a");
            webElement.SendKeys(text);
        }

        public void SelectByValueFromDropDown(By element, string value)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
            SelectElement selectElement = new SelectElement(BaseTestContext.Driver.FindElement(element));
            ScrollIntoView(BaseTestContext.Driver.FindElement(element));
            selectElement.SelectByValue(value);
        }

        public string GetElementText(By element)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            return BaseTestContext.Driver.FindElement(element).Text.Trim();
        }

        public string GetElementValueAttribute(By element, string attributeName)
        {
            return BaseTestContext.Driver.FindElement(element).GetAttribute(attributeName);
        }

        public IReadOnlyCollection<IWebElement> GetWebElements(By element)
        {
            return BaseTestContext.Driver.FindElements(element);
        }

        public IWebElement GetWebElement(By element)
        {
            return BaseTestContext.Driver.FindElement(element);
        }

    }
}
