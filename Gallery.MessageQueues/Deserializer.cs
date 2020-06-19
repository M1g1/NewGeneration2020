using System.Text;
using Newtonsoft.Json;

namespace Gallery.MessageQueues
{
    public class Deserializer
    {
        public static T DeserializeToObject<T>(string obj) where T : class
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
        public static string DeserializeToString(byte[] obj)
        {
            return Encoding.UTF8.GetString(obj);
        }
    }
}