using log4net;
using NuGet.Protocol.Plugins;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SampleMSTestProject.Utilities
{
    public class RestAPIHelperLibrary
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IRestRequest GetRestRequest(string endPointUrl, Method method, string authToken, string apiKey, string messageID, object objBody, string sessionId="null",string clientBatchId = null)
        {
            IRestRequest restRequest = new RestRequest(endPointUrl, method);
            restRequest.AddHeader("Authorization", authToken);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("x-APIKey", apiKey);
            restRequest.AddHeader("x-RequestTimeStamp", DateTime.Now.ToString("dd/mm/yyyy"));
            if(objBody != null)
            {
                restRequest.AddJsonBody(objBody, "application/json");
            }

            return restRequest;
        }

        public static IRestResponse PostRequest(IRestRequest restRequest)
        {
            IRestClient restClient = new RestClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            return restClient.Post(restRequest);
        }


        public static string GetOAuthToken(string environment)
        {
            RestClient client = new RestClient("AuthTokenUrl");
            RestRequest request = new RestRequest() { Method = Method.POST };
            request.AddParameter("grant_type", "client_credentials", ParameterType.GetOrPost);
            request.AddParameter("Client_id", "AuthClientID", ParameterType.GetOrPost);
            request.AddParameter("Client_secret", "AuthSecretKey", ParameterType.GetOrPost);
            GetOAuthToken authToken = Utility
        }
    }
}
