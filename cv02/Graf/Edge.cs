using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cv02.Graf
{
    public class Edge<T>
    {
        public T Name { get; set; }
        public Vertex<T> StartVertex { get; set; }
        public Vertex<T> EndVertex { get; set; }

        public Edge(T name, Vertex<T> startVertex, Vertex<T> endVertex)
        {
            Name = name;
            StartVertex = startVertex;
            EndVertex = endVertex;
        }

        public override string ToString()
        {
            return Name + ": " + StartVertex + ", " + EndVertex;
        }

        public bool sameEdge(Edge<T> other)
        {
            return StartVertex.Name.Equals(other.StartVertex.Name) && EndVertex.Name.Equals(other.EndVertex.Name);
        }
    }
}
