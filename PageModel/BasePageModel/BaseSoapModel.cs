using log4net;
using Newtonsoft.Json;
using SampleMSTestProject.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SampleMSTestProject.PageModel.BasePageModel
{
    public class BaseSoapModel : BaseModel
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private EnvironmentDetails environment;

        public BaseSoapModel(BaseTestContext baseTextContext) : base(baseTextContext)
        {
            //baseUrl = ""; 

        }


        public string BaseUrl { get; set; }

        public HttpWebRequest SoapRequest { get; set; }

        public HttpWebResponse SoapResponse { get; set; }

        public string ResponseString { get; set; }

        public string SoapAction { get; set; }

        public virtual void GetResponseObject()
        {

        }

        public void SendRequest(object requestModel, string soapAction, MethodAccessException method)
        {
            try
            {
                SoapRequest = SoapAPIHelperLibrary.GetSOAPRequest(BaseUrl, requestModel, soapAction, method);
                SoapResponse = SoapAPIHelperLibrary.GetSoapRespone(SoapRequest);

                ResponseString = SoapAPIHelperLibrary.GetResponseAsString(SoapResponse);
                string responseCode = ResponseString.Split('~')[0];
                ResponseString = ResponseString.Split('~')[1];

                if (responseCode.Equals("Success")){
                    LogPassXMLBlock(log, "Send Request Action is Successfull", ResponseString);
                }
                else
                {
                    LogFailXMLBlock(log, "Send Request Action has Failed", ResponseString);
                }
            }
            catch (Exception ex)
            {
                LogFailureMessage(log, ex.ToString());
            }
            finally
            {
                LogAPIData();
            }

        }

        private void LogAPIData()
        {
            LogInfo(log, string.Format("Request Complete, Request: {0}, Response: {2}", JsonConvert.SerializeObject(SoapRequest, Formatting.Indented), JsonConvert.SerializeObject(SoapResponse, Formatting.Indented)));
        }
    }
}
