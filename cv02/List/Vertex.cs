using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace cv02.Lists
{
    public class Vertex
    {
        public string Name { get; set; }
        public int coordinateX { get; set; }
        public int coordinateY { get; set; }
        public Point point;
        public Rectangle rectangle;


        public Vertex(string name)
        {
            Name = name;
        }

        public void generateCoordinates(int width, int height)
        {
            Random r = new Random();
            var coordinateX = r.Next(0, width);
            this.coordinateX = coordinateX;
            var coordinateY = r.Next(0, height);
            this.coordinateY = coordinateX;

            var radius = 10;

            this.point = new Point(this.coordinateX, this.coordinateY);
            this.rectangle = new Rectangle(coordinateX, coordinateX, radius, radius);

        }

        public void setPoint(Point point)
        {
            this.point = point;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool sameVertex(Vertex other)
        {
            return this.Name.Equals(other.Name);
        }

    }
}
