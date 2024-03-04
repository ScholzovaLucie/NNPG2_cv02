using cv02.Lists;
using System.Drawing;
using System.Drawing.Drawing2D;
using Path = cv02.Lists.Path;

namespace cv02
{
    public partial class Form1 : Form
    {
        private GraphProcessor graphProcessor { get; set; }
        private Vertex vertex = null;
        private bool dragging = false;


        public Form1(GraphProcessor graphProcessor)
        {
            this.graphProcessor = graphProcessor;
            InitializeComponent();
        }
        Graphics g;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Draw(g);

        }

   

        private void Draw(Graphics g)
        {
                 if (dragging && vertex != null)
            {
                g.FillRectangle(Brushes.Green, vertex.rectangle);
            }
            else
            {
                foreach (Vertex item in this.graphProcessor.graphData.Vertices)
                {
                    item.generateCoordinates(this.Width, this.Height);
                    g.FillRectangle(Brushes.Blue, item.rectangle);
                }
            }
            
        }



        private void MouseDown(object sender, MouseEventArgs e)
        {
            foreach (Vertex item in this.graphProcessor.graphData.Vertices)
            {
                if (item.rectangle.Contains(e.Location))
                {
                    dragging = true;
                    item.point = new Point(e.X - item.rectangle.X, e.Y - item.rectangle.Y);
                    this.vertex = item;
                }
            }
               
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            this.Invalidate();
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                foreach (Vertex item in this.graphProcessor.graphData.Vertices)
                {
                    if (item.sameVertex(this.vertex))
                    {
                        item.rectangle.Location = new Point(e.X - item.point.X, e.Y - item.point.Y);
                        this.Invalidate();
                    }
                    
                }
                    
            }
        }

        private void MouseWheel(object sender, MouseEventArgs e)
        {
            // TODO    
        }


    }
}