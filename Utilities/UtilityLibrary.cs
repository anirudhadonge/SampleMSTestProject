using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SampleMSTestProject.Utilities.CustomerData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SampleMSTestProject.Utilities
{
    public class UtilityLibrary
    {
        public static T GetDeSerializedObject<T>(string responseString)
        {
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public static string GetSerializedString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetSerializedXMLString(object envelope)
        {
            try
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("soapenv", "https://schemas.xmlsoap.org/soap/envelope/");
                ns.Add("v2", "https://schemas.xmlsoap.org/soap/envelope/");
                ns.Add("v4", "https://schemas.xmlsoap.org/soap/envelope/");
                ns.Add("arr", "https://schemas.xmlsoap.org/soap/envelope/");
                ns.Add("xsi", "https://schemas.xmlsoap.org/soap/envelope/");
                ns.Add("xsd", "https://schemas.xmlsoap.org/soap/envelope/");


                XmlSerializer xmlSerializer = new XmlSerializer(envelope.GetType());

                var settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;

                string xmlString;

                using (var stringWriter = new StringWriter())
                {
                    using(XmlWriter writer = XmlWriter.Create(stringWriter, settings))
                    {
                        xmlSerializer.Serialize(writer, envelope, ns);
                        xmlString = stringWriter.ToString();
                    }
                }
                return xmlString;
            }catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static BaseCustomerData GetCustomerData(string testName)
        {
            JObject testDataFile = GetJsonObjectOfTestFile(testName);
            return CreateCustomerDataFromJson(testDataFile);
        }

        private static JObject GetJsonObjectOfTestFile(string testName)
        {
            string testJsonFileName = Path.Combine(Directory.GetCurrentDirectory(), "FolderName", "PortalName", testName + ".json");
            return new JObject();
        }

        public static BaseCustomerData CreateCustomerDataFromJson(JObject testDataFile)
        {
            return new BaseCustomerData();
        }

    }
}
