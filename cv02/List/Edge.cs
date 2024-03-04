using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cv02.Lists
{
    public class Edge
    {
        public string Name { get; set; }
        public Vertex StartVertex { get; set; }
        public Vertex EndVertex { get; set; }

        public Edge(string name, Vertex startVertex, Vertex endVertex)
        {
            Name = name;
            StartVertex = startVertex;
            EndVertex = endVertex;
        }

        public override string ToString()
        {
            return Name + ": " + StartVertex + ", " + EndVertex;
        }

        public bool sameEdge(Edge other)
        {
            return this.StartVertex.Name.Equals(other.StartVertex.Name) && this.EndVertex.Name.Equals(other.EndVertex.Name);
        }
    }
}
