using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SampleMSTestProject.Utilities
{
    public class SoftAssert : BaseLoggerLib
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SoftAssert(BaseTestContext baseTextContext) : base(baseTextContext)
        {
            FailedAssertList = new List<string>();
            PassedAssertList = new List<string>();
            AssertMessage = "Exception '{key}': {expectedValue},Actual:{actualValue}";
        }

        private List<string> FailedAssertList { get; set; }
        private List<string> PassedAssertList { get; set; }
        private string AssertMessage { get; set; }

        public void AreEqual(string key, object expectedValue, object actualValue)
        {
            string assertString = GetAssertMessage(key, expectedValue.ToString(), actualValue.ToString());
            try
            {
                Assert.AreEqual(expectedValue, actualValue);
                PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                log.Error(exception.ToString());
                FailedAssertList.Add(assertString);
            }
        }

        public void IsContains(string key, object expectedValue, object actualValue)
        {
            string assertString = GetAssertMessage(key, expectedValue.ToString(), actualValue.ToString());
            try
            {
                Assert.IsTrue(expectedValue.ToString().Contains(actualValue.ToString()));
                PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                log.Error(exception.ToString());
                FailedAssertList.Add(assertString);
            }
        }

        public void IsNotNull(string key, object actualValue)
        {
            string assertString = GetAssertMessage(key, "Not Null", actualValue.ToString());
            try
            {
                Assert.IsNotNull(actualValue);
                PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                log.Error(exception.ToString());
                FailedAssertList.Add(assertString);
            }
        }

        public void IsEmpty(string key, object actualValue)
        {
            string assertString = GetAssertMessage(key, "Empty", actualValue.ToString());
            try
            {
                Assert.IsTrue(string.IsNullOrEmpty(actualValue.ToString()));
                PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                log.Error(exception.ToString());
                FailedAssertList.Add(assertString);
            }
        }

        public void IsTrue(string key, bool condition)
        {
            string assertString = GetAssertMessage(key, "true", condition.ToString());
            try
            {
                Assert.IsTrue(condition);
                PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                log.Error(exception.ToString());
                FailedAssertList.Add(assertString);
            }
        }

        public void IsFalse(string key, bool condition)
        {
            string assertString = GetAssertMessage(key, "false", condition.ToString());
            try
            {
                Assert.IsFalse(condition);
                PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                log.Error(exception.ToString());
                FailedAssertList.Add(assertString);
            }
        }

        public void IsMatch(string key, object expectedValue, object actualValue)
        {
            string assertString = GetAssertMessage(key, expectedValue.ToString(), actualValue.ToString());
            try
            {
                if (Regex.IsMatch(actualValue.ToString(), expectedValue.ToString()))
                {
                    PassedAssertList.Add(assertString);
                }
                else
                {
                    throw new Exception(assertString);
                }
            }
            catch (Exception exception)
            {
                log.Error(exception.ToString());
                FailedAssertList.Add(assertString);
            }
        }

        public void IsNullOrEmptyOrString(string key, string expectedValue, string actualValue)
        {
            if (string.IsNullOrEmpty(expectedValue.Trim()))
            {
                IsEmpty(key, actualValue);
            }
            else
            {
                AreEqual(key, expectedValue, actualValue);
            }
        }

        public void AddMessage(string message)
        {
            PassedAssertList.Add(message);
        }

        public void IsDBNull(string key, object actualValue)
        {
            string assertString = GetAssertMessage(key, string.Empty, actualValue.ToString());
            try
            {
                Assert.IsTrue(actualValue == DBNull.Value);
                PassedAssertList.Add(assertString);
            }
            catch (Exception exception)
            {
                log.Error(exception.ToString());
                FailedAssertList.Add(assertString);
            }
        }

        private string GetListString(List<string> listString)
        {
            try
            {
                return listString.Aggregate((a, b) => a + "<br>\n" + b);
            }
            catch
            {
                return "List was Empty";
            }
        }

        public void AssertAll()
        {
            LogSuccessMessage(log, GetListString(PassedAssertList));
            Assert.IsTrue(FailedAssertList.Count == 0, "<br>\n" + GetListString(FailedAssertList) + "<br>\n");
        }

        private String GetAssertMessage(string key, string expectedValue, string actualValue)
        {
            return AssertMessage.Replace("{expectedValue}", expectedValue).Replace("{actualValue}", actualValue).Replace("{key}", key);
        }
    }
}
