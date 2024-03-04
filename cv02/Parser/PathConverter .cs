using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Path = cv02.Lists.Path;
using RList = cv02.Graf.RList;


namespace cv02
{
    public class PathConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Path);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Path path)
            {
                JObject obj = new JObject();
                obj.Add("Name", path.Name);
                obj.WriteTo(writer);
            }
            else if (value is RList rList)
            {
                JArray array = new JArray();
                foreach (var paths in rList.List)
                {
                    JObject obj = new JObject();
                    obj.Add("Name", paths.First().Name);
                    array.Add(obj);
                }
                array.WriteTo(writer);
            }
        }
    }
}
