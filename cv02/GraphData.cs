using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cv02.Lists;

namespace cv02
{
    public class GraphData
    {

        public List<Vertex> Vertices { get; private set; }
        public List<Vertex> InputVertices { get; private set; }
        public List<Vertex> OutputVertices { get; private set; }
        public List<List<Vertex>> Cross {  get; private set; }
        public List<Edge> Edges { get; private set; }

        private Data data;

        public GraphData(Data data)
        {
            Vertices = new List<Vertex>();
            InputVertices = new List<Vertex>();
            OutputVertices = new List<Vertex>();
            Cross = new List<List<Vertex>>();
            Edges = new List<Edge>();
            this.data = data;
            createGrafData();
        }

        private void createGrafData()
        {
            if (data.Vertices != null)
            {
                for (int i = 0; i < data.Vertices.Length; i++)
                {
                    this.Vertices.Add(new Vertex(data.Vertices[i]));
                }
            }
            
            if (data.InputVertices != null)
            {
                for (int i = 0; i < data.InputVertices.Length; i++)
                {
                    this.InputVertices.Add(new Vertex(data.InputVertices[i]));
                }
            }
            if (data.OutputVertices != null)
            {
                for (int i = 0; i < data.OutputVertices.Length; i++)
                {
                    this.OutputVertices.Add(new Vertex(data.OutputVertices[i]));
                }
            }
            
            if (data.Cross != null)
            {
                for (int i = 0; i < data.Cross.Length; i++)
                {
                    List<Vertex> verzexes = new List<Vertex>();
                    foreach (var item in data.Cross[i])
                    {
                        verzexes.Add(new Vertex(item));
                    }
                    this.Cross.Add(verzexes);
                }
            }
            
            if (data.Edges != null)
            {
                for (int i = 0; i < data.Edges.Length; i++)
                {
                    Vertex start = new Vertex(data.Edges[i][0]);
                    Vertex end = new Vertex(data.Edges[i][1]);
                    string name = "E" + i.ToString();
                    this.Edges.Add(new Edge(name, start, end));
                }
            }
        }

        public Vertex getVertexByName(string name, List<Vertex> list)
        {
            foreach (Vertex vertex in list)
            {
                if (vertex.Name.Equals(name))
                {
                    return vertex;
                }
            }

            return null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Vertices:");
            foreach (var vertex in Vertices)
            {
                sb.AppendLine(vertex.ToString());
            }

            sb.AppendLine("Input Vertices:");
            foreach (var inputVertex in InputVertices)
            {
                sb.AppendLine(inputVertex.ToString());
            }

            sb.AppendLine("Output Vertices:");
            foreach (var outputVertex in OutputVertices)
            {
                sb.AppendLine(outputVertex.ToString());
            }

            sb.AppendLine("Cross:");
            foreach (var cross in Cross)
            {
                sb.AppendLine(cross.ToString());
            }

            sb.AppendLine("Edges:");
            foreach (var edge in Edges)
            {
                sb.AppendLine(edge.ToString());
            }

            return sb.ToString();
        }
    }
}
