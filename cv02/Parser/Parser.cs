using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using cv02.Graf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cv02.Parser
{
    public class Parser
    {
        public Parser() { }
        public Data loadData(string filePath)
        {
            string jsonText = File.ReadAllText(filePath);
            Data data = JsonConvert.DeserializeObject<Data>(jsonText);
            return data;
        }

        public void saveData(RList rlist, LList llist)
        {

            var dataToSerialize = new
            {
                LList = llist.List.Select(path => new { PathName = path.Name, Vertices = path.Vertices.Select(v => v.Name).ToList() }),
                RList = rlist.List.Select(path => new { DisjunktPaths = path })
            };

            // Převod na JSON formát
            string json = JsonConvert.SerializeObject(dataToSerialize, Formatting.Indented, new PathConverter());

            // Uložení do souboru
            File.WriteAllText("../../../files/resultFile.json", json);
        }
    }
}
