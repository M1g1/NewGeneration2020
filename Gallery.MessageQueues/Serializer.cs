using System.Text;
using Newtonsoft.Json;

namespace Gallery.MessageQueues
{
    public class Serializer
    {
        public static string SerializeToJson<T>(T obj) where T : class
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static byte[] SerializeToBytes(string obj)
        {
            return Encoding.UTF8.GetBytes(obj);
        }
    }
}