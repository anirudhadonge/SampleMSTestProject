using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using SampleMSTestProject.Enums;
using SampleMSTestProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleMSTestProject.PageModel.BasePageModel
{
    public abstract class BaseAPIModel : BaseModel
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BaseAPIModel(BaseTestContext baseTextContext) : base(baseTextContext)
        {
        }

        public IRestClient RestClient { get; set; }

        public IRestRequest RestRequest { get; set; }

        public IRestResponse RestResponse { get; set; }

        public string EndPointUrl { get; set; }

        public string OAuthToken { get; set; }

        public string MessageID { get; set; }

        public Object ObjectModel { get; set; }

        public string ApiKey { get; set; }

        public string Description { get; set; }


        public void SendRequest(object ObjectModel, Method method, string sessionTokenId = null, string clientBatchId = null)
        {
            try
            {
                RestRequest = RestAPIHelperLibrary.GetRestRequest(EndPointUrl, method, OAuthToken, ApiKey, MessageID.Trim(), ObjectModel, sessionTokenId, clientBatchId);
                RestResponse = RestAPIHelperLibrary.GetRestResponse(RestRequest, method);
                LogAPIData(RestRequest, RestResponse);
                LogInfoMessage(log, RestResponse.Content);
            }
            catch (Exception ex)
            {
                LogFailureMessage(log, ex.ToString());
            }
        }

        public virtual void ValidateResponseStatus(string responseStatusCode)
        {
            Assert.AreEqual(responseStatusCode, APIStatusCode.Created.ToString());
        }

        public virtual void ValidateResponseDescription(string actualString)
        {
            Assert.AreEqual(this.Description, actualString);
            LogSuccessMessage(log, "Excepted Description :" + Description + " Actual Description :" + actualString);
        }

        public void ValidateResponseHeader(IRestResponse response, BaseTestContext baseTestContext)
        {
            Dictionary<string, string> headersToValidate = new Dictionary<string, string>();
            headersToValidate.Add("cache-control", "no-store, no-cache");

            var headers = RestResponse.Headers;

            LogInfoMessage(log, "******************* vakudate Response Headers *********************");
            SoftAssert softAssert = new SoftAssert(baseTestContext);

            foreach (string key in headersToValidate.Keys)
            {
                string expectedValue = headersToValidate[key];
                string actualValue = (from header in headers
                                      where header.Name == key
                                      select header.Value.ToString()).FirstOrDefault();

                softAssert.AreEqual(key, expectedValue, actualValue);
            }
            softAssert.AssertAll();
        }

        private void LogAPIData(IRestRequest request, IRestResponse response)
        {
            var requestToLog = new
            {
                resource = request.Resource,
                parameters = request.Parameters.Select(Parameter => new
                {
                    name = Parameter.Name,
                    value = Parameter.Value,
                    type = Parameter.Type.ToString()
                }),
                method = request.Method.ToString()

            };

            var responseToLog = new
            {
                statusCode = response.StatusCode,
                content = response.Content,
                headers = response.Headers.Select(header => new
                {
                    name = header.Name,
                    value = header.Value,
                    type = header.Type.ToString()
                }),
                responseUri = response.ResponseUri,
                errorMessage = response.ErrorMessage
            };

            LogInfo(log, string.Format("Request completed, Request: {0}, Response:{1}", UtilityLibrary.GetSerializedString(requestToLog), UtilityLibrary.GetSerializedString(responseToLog))

        }

    }
}
