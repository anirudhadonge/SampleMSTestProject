using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
