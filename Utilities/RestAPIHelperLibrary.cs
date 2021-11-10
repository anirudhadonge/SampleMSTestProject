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

        public static object AuthToken { get; private set; }

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

        public static IRestResponse DeleteRequest(IRestRequest restRequest)
        {
            IRestClient restClient = new RestClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            return restClient.Delete(restRequest);
        }

        public static IRestResponse PutRequest(IRestRequest restRequest)
        {
            IRestClient restClient = new RestClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            return restClient.Put  (restRequest);
        }

        public static IRestResponse GetRequest(IRestRequest restRequest)
        {
            IRestClient restClient = new RestClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            return restClient.Get(restRequest);
        }


        public static IRestResponse GetRestResponse(IRestRequest restRequest,Method method)
        {
            switch (method)
            {
                case Method.POST:
                    return PostRequest(restRequest);
                case Method.GET:
                    return GetRequest(restRequest);
                case Method.PUT:
                    return PutRequest(restRequest);
                case Method.DELETE:
                    return DeleteRequest(restRequest);

            }
            return null;
        }

        public static string GetOAuthToken(string environment)
        {
            RestClient client = new RestClient("AuthTokenUrl");
            RestRequest request = new RestRequest() { Method = Method.POST };
            request.AddParameter("grant_type", "client_credentials", ParameterType.GetOrPost);
            request.AddParameter("Client_id", "AuthClientID", ParameterType.GetOrPost);
            request.AddParameter("Client_secret", "AuthSecretKey", ParameterType.GetOrPost);
            Object authToken = UtilityLibrary.GetDeSerializedObject<object>(client.Execute(request).Content);
            return authToken.ToString();
        }
    }
}
