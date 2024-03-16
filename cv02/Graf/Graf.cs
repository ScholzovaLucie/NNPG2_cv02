using cv02.Graf;
using cv02.Path;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cv02.Graf
{
    public class Graf<T, TVertexData, TRdgeData>
    {
        private List<Vertex<T, TVertexData, TRdgeData>> vertices;

        public List<Vertex<T, TVertexData, TRdgeData>> Vertices { get { return vertices; } }
        private List<List<Vertex<T, TVertexData, TRdgeData>>> cross;
        public List<List<Vertex<T, TVertexData, TRdgeData>>> Cross { get { return cross; } }

        public int size { get { return vertices.Count; } }

        public Graf()
        {
            this.vertices = new List<Vertex<T, TVertexData, TRdgeData>>();
            this.cross = new List<List<Vertex<T, TVertexData, TRdgeData>>>();
        }

        public void AddVertex(Vertex<T, TVertexData, TRdgeData> vertex)
        {
            vertices.Add(vertex);
        }

        public void UpdateVertex(Vertex<T, TVertexData, TRdgeData> vertex)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].Name.Equals(vertex.Name)) vertices[i] = vertex;
            }

        }

        public void RemoveVertex(Vertex<T, TVertexData, TRdgeData> vertex)
        {
            foreach (var v in vertices)
            {
                foreach (var edge in v.Edges.ToList())
                {
                    if (edge.EndVertex == vertex)
                    {
                        var nextVertex = vertices.FirstOrDefault(v => v.Edges.Any(e => e.StartVertex == vertex));
                        if (nextVertex != null)
                        {
                            edge.EndVertex = nextVertex;
                            nextVertex.Edges.Add(edge);
                        }
                        v.Edges.Remove(edge);
                    }
                }
            }
            vertices.Remove(vertex);
        }

        public bool HasVertex(Vertex<T, TVertexData, TRdgeData> vertex)
        {
            return vertices.Contains(vertex);
        }

        public Vertex<T, TVertexData, TRdgeData> findVertexByName(T name)
        {
            foreach (var item in vertices)
            {
                if (item.Name.Equals(name)) return item;
            }
            return null;
        }
    }
}
