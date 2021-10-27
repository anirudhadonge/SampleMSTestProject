using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SampleMSTestProject.Utilities
{
    public class SoapAPIHelperLibrary
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static HttpWebRequest GetSOAPRequest(string baseURL,object requestModel,string soapAction,MethodAccessException method)
        {
            XmlDocument reqBody = new XmlDocument();
            reqBody.LoadXml(UtilityLibrary.GetSerializedXMLString(requestModel));
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            request.ContentType = "text/xml;charset=UTF-8";
            request.Headers.Add("SOAPAction", soapAction);
            request.Accept = "text/xml";
            request.Method = method.ToString();

            using(Stream stream = request.GetRequestStream())
            {
                reqBody.Save(stream);
            }

            return request;
        }

        public static HttpWebResponse GetSoapRespone(HttpWebRequest request)
        {
            return (HttpWebResponse)request.GetResponse();
        }

        internal static string GetResponseAsString(HttpWebResponse response)
        {
            string responseText;

            using(var reader = new StreamReader(response.GetResponseStream()))
            {
                responseText = reader.ReadToEnd();
            }

            try
            {
                XDocument document = XDocument.Parse(responseText);
                var nodes = document.Descendants();
                string responseCode = (from node in nodes
                                       where node.Name.LocalName == "ResponseCode"
                                       select node).FirstOrDefault().Value;

                return responseCode + "~" + document.ToString();
            }catch(Exception ex)
            {
                return ex.Message+ responseText;
            }
        }
    }
}
