﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cv02.DrawData
{
    internal class VertexData
    {
        public int coordinateX { get; set; }
        public int coordinateY { get; set; }
        public Point point;
        public Rectangle rectangle;
        public Rectangle magneticRectangle { get; set; }
        private const int defaultMagneticSize = 40;


        public VertexData() {
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


        public void setCoordinateX(int coordinateX)
        {
            this.coordinateX = coordinateX;
            updateMagneticRectangle();
        }

        public void setCoordinateY(int coordinateY)
        {
            this.coordinateY = coordinateY;
            updateMagneticRectangle();
        }

        private void updateMagneticRectangle()
        {
            this.magneticRectangle = new Rectangle(
                this.rectangle.X - defaultMagneticSize / 2,
                this.rectangle.Y - defaultMagneticSize / 2,
                this.rectangle.Width + defaultMagneticSize,
                this.rectangle.Height + defaultMagneticSize
            );
        }
    }
}
