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
    public class Parser<T>
    {
        private Data<T> data { get; set; }
        private List<Vertex<T>> vertices { get; set; }

        public Parser(string filePath)
        {
            try
            {
                string jsonData = System.IO.File.ReadAllText(filePath);
                data = JsonConvert.DeserializeObject<Data<T>>(jsonData);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba při parsování dat ze souboru: " + ex.Message);
            }
        }

        public List<Vertex<T>> ExtractVertices()
        {
            try
            {
                vertices = new List<Vertex<T>>();
                foreach (T vertexName in data.Vertices)
                {
                    vertices.Add(new Vertex<T>(vertexName));
                }
                return vertices;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba při parsování dat ze souboru: " + ex.Message);
                return null;
            }

        }

        public List<Edge<T>> ExtractEdges()
        {
            try
            {

                List<Edge<T>> edges = new List<Edge<T>>();
                foreach (T[] edgeArray in data.Edges)
                {
                    T fromVertexName = edgeArray[0];
                    T toVertexName = edgeArray[1];

                    Vertex<T> fromVertex = vertices.Find(v => EqualityComparer<T>.Default.Equals(v.Name, fromVertexName));
                    Vertex<T> toVertex = vertices.Find(v => EqualityComparer<T>.Default.Equals(v.Name, toVertexName));
                    T name = (T)Convert.ChangeType("E" + edges.Count.ToString(), typeof(T));

                    if (fromVertex != null && toVertex != null)
                    {
                        edges.Add(new Edge<T>(name, fromVertex, toVertex));
                    }
                    else
                    {
                        throw new Exception("Nepodařilo se najít vrcholy pro hranu.");
                    }
                }
                return edges;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba při parsování dat ze souboru: " + ex.Message);
                return null;
            }
        }

        public List<Vertex<T>> ExtractInputVertices()
        {
            try
            {
                List<Vertex<T>> inputVertices = new List<Vertex<T>>();
                foreach (T inputVertexName in data.InputVertices)
                {
                    Vertex<T> inputVertex = vertices.Find(v => EqualityComparer<T>.Default.Equals(v.Name, inputVertexName));
                    if (inputVertex != null)
                    {
                        inputVertices.Add(inputVertex);
                    }
                }
                return inputVertices;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba při parsování dat ze souboru: " + ex.Message);
                return null;
            }
        }

        public List<Vertex<T>> ExtractOutputVertices()
        {
            try
            {
                List<Vertex<T>> outputVertices = new List<Vertex<T>>();
                foreach (T outputVertexName in data.OutputVertices)
                {
                    Vertex<T> outputVertex = vertices.Find(v => EqualityComparer<T>.Default.Equals(v.Name, outputVertexName));
                    if (outputVertex != null)
                    {
                        outputVertices.Add(outputVertex);
                    }
                }
                return outputVertices;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba při parsování dat ze souboru: " + ex.Message);
                return null;
            }
        }

        public List<List<Vertex<T>>> ExtractCross()
        {
            try
            {

                List<List<Vertex<T>>> cross = new List<List<Vertex<T>>>();
                foreach (T[] crossArray in data.Cross)
                {
                    List<Vertex<T>> crossList = new List<Vertex<T>>();
                    foreach (T crossItem in crossArray)
                    {
                        Vertex<T> crossVertex = vertices.Find(v => EqualityComparer<T>.Default.Equals(v.Name, crossItem));
                        if (crossVertex != null)
                        {
                            crossList.Add(crossVertex);
                        }
                    }
                    cross.Add(crossList);
                }
                return cross;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba při parsování dat ze souboru: " + ex.Message);
                return null;
            }
        }

    }
}


