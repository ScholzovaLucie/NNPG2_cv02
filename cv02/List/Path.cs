using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cv02.Lists
{
    public class Path
    {
        public string Name { get; set; }
        public LinkedList<Vertex> Vertices { get; set; }

        public Path(LinkedList<Vertex> vertices)
        {
            Vertices = vertices;
        }

        public Path(int index, LinkedList<Vertex> vertices)
        {
            Name = "A" + index;
            Vertices = vertices;
        }


        public void setName(int index)
        {
            Name = "A" + index;
        }

        public Path Copy()
        {
            return new Path(new LinkedList<Vertex>(Vertices));
        }

        public bool Equals(Path other)
        {
            if (Vertices.Count != other.Vertices.Count) return false;

            bool same = true;

            Vertex aktual_this = Vertices.First();
            Vertex aktual_other = other.Vertices.First();

            if (!aktual_this.Name.Equals(aktual_other.Name))
            {
                return false;
            }

            foreach (Vertex s in Vertices)
            {
                if (Vertices.Find(aktual_this).Next == null)
                {
                    if (!aktual_this.Name.Equals(aktual_other.Name))
                    {
                        return false;
                    }
                    return same;
                }

                aktual_this = Vertices.Find(aktual_this).Next.Value;
                aktual_other = other.Vertices.Find(aktual_other).Next.Value;

                if (!aktual_this.Name.Equals(aktual_other.Name))
                {
                    return false;
                }

            }
            return same;
        }

        public bool IsDisjoint(Path other)
        {
            foreach (var vertex in Vertices)
            {
                foreach (var vertex1 in other.Vertices)
                {
                    if (vertex1.Name.Equals(vertex.Name)) { return false; }
                }
            }

            return true;
        }
    }
}
