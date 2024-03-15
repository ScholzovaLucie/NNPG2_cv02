using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cv02.Graf
{
    public class Vertex<T>
    {
        public T Name
        {
            get { return name; }
            set { name = value; }
        }
        public int coordinateX { get; set; }
        public int coordinateY { get; set; }
        public Point point;
        public Rectangle rectangle;

        private T name;

        public List<Edge<T>> Edges {  
            get { return edges;}
            set { edges = value; }
        }

        private List<Edge<T>> edges;

        public Vertex(T name) {
            this.name = name;
            edges = new List<Edge<T>>();
        }

        public void generateCoordinates(int start, int width, int height)
        {
            Random rnd = new Random();
            lock (rnd)
            {
                this.coordinateX = rnd.Next(start, width);
            }
            lock (rnd)
            {
                this.coordinateY = rnd.Next(0, height);
            }
        }

        public bool sameVertex(Vertex<T> other)
        {
            return Name.Equals(other.Name);
        }

        public override string ToString()
        {
            return Convert.ToString(Name);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17; 
                hash = hash * 23 + (Name != null ? Name.GetHashCode() : 0);
                return hash;
            }
        }


    }
}
