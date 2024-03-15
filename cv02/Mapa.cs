using cv02.Graf;
using cv02.Path;
using System;
using System.CodeDom;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms.VisualStyles;
using Path = cv02.Path;

namespace cv02
{
    public partial class Mapa : Form
    {
        private GraphProcessor<string> graphProcessor { get; set; }
        private Vertex<string> vertex = null;
        private bool dragging = false;
        private Graphics g;
        private bool moove = false;
        private int radius = 10;
        private int border = 200;

        public Mapa()
        {
            this.graphProcessor = new GraphProcessor<string>();
            graphProcessor.ProcessGraph("../../../files/sem_01_velky.json");
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
            this.PaintPanel.MouseDown += Form1_MouseDown;
            this.PaintPanel.MouseMove += Form1_MouseMove;
            this.PaintPanel.MouseUp += Form1_MouseUp;
            this.PaintPanel.Paint += Draw;
            this.count_paths.Text = this.graphProcessor.paths.paths.Count().ToString();
            this.DisjunktPathsCount.Text = this.graphProcessor.DisjunktPaths.disjointPaths.Count().ToString();

            foreach (Vertex<string> item in this.graphProcessor.graphData.Vertices)
            {
                if (this.graphProcessor.InputVertices.Contains(item))
                {
                    item.generateCoordinates(0, border, this.PaintPanel.ClientSize.Height - radius);
                }
                else if (this.graphProcessor.OutputVertices.Contains(item))
                {
                    item.generateCoordinates(this.PaintPanel.ClientSize.Width - radius - border, this.PaintPanel.ClientSize.Width - radius, this.PaintPanel.ClientSize.Height - radius);
                }
                else
                {
                    item.generateCoordinates(200, this.PaintPanel.ClientSize.Width - radius - border, this.PaintPanel.ClientSize.Height - radius);
                }

                item.rectangle = new Rectangle(item.coordinateX, item.coordinateY, radius, radius);
            }

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            bool found = false;

            foreach (Vertex<string> item in this.graphProcessor.graphData.Vertices)
            {
                if (item.rectangle.Contains(e.Location))
                {
                    this.vertex = item;

                    found = true;
                    if (moove)
                    {
                        dragging = true;
                        this.vertex.point = new Point(e.X - item.rectangle.X, e.Y - item.rectangle.Y);
                    }
                    break;
                }
            }


            if (!found)
            {
                this.vertex = null;
            }
            this.PaintPanel.Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            moove = false;
            dragging = false;
            this.PaintPanel.Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                this.vertex.rectangle.Location = new Point(e.X - this.vertex.point.X, e.Y - this.vertex.point.Y);
                this.graphProcessor.graphData.UpdateVertex(this.vertex);
                this.PaintPanel.Invalidate();
            }
        }

        protected void Draw(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            if (dragging && vertex != null)
            {
                g.FillRectangle(Brushes.Green, vertex.rectangle);

                foreach (Vertex<string> item in this.graphProcessor.graphData.Vertices)
                {
                    if (!item.Equals(this.vertex))
                    {
                        g.FillRectangle(Brushes.Blue, item.rectangle);
                    }
                }
            }
            else
            {
                redrawVertices(g); // VykreslÌ vrcholy
                redrawEdges(g);    // VykreslÌ hrany
            }
        }

        private void redrawVertices(Graphics g)
        {
            Font drawFont = new Font("Arial", 7);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();


            for (int i = 0; i < this.graphProcessor.graphData.Vertices.Count; i++)
            {
                Vertex<string> item = this.graphProcessor.graphData.Vertices[i];
                if (item.Equals(this.vertex))
                {
                    g.FillRectangle(Brushes.Red, item.rectangle);
                    g.DrawString(item.Name, drawFont, drawBrush, item.rectangle.X - radius, item.rectangle.Y - radius, drawFormat);
                    item.coordinateX = item.rectangle.X;
                    item.coordinateY = item.rectangle.Y;
                }
                else
                {
                    if (this.graphProcessor.InputVertices.Contains(item))
                    {
                        g.FillRectangle(Brushes.Green, item.rectangle);
                    }
                    else if (this.graphProcessor.OutputVertices.Contains(item))
                    {
                        g.FillRectangle(Brushes.Yellow, item.rectangle);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.Blue, item.rectangle);
                    }

                    foreach (var crossList in this.graphProcessor.graphData.Cross)
                    {
                        if (crossList[1] == item)
                        {
                            g.FillRectangle(Brushes.Purple, item.rectangle);
                        }
                    }

                    g.DrawString(item.Name, drawFont, drawBrush, item.coordinateX - radius, item.coordinateY - radius, drawFormat);

                }
            }



        }
        protected void redrawEdges(Graphics g)
        {
            Pen pen = new Pen(Brushes.Black, 2); // NastavÌme barvu a tlouöùku Ë·ry

            foreach (Vertex<string> v in this.graphProcessor.graphData.Vertices)
            {
                foreach (Edge<string> edge in v.Edges)
                {

                    g.DrawLine(pen, edge.StartVertex.rectangle.X + (edge.StartVertex.rectangle.Width / 2),
                                      edge.StartVertex.rectangle.Y + (edge.StartVertex.rectangle.Height / 2),
                                      edge.EndVertex.rectangle.X + (edge.EndVertex.rectangle.Width / 2),
                                      edge.EndVertex.rectangle.Y + (edge.EndVertex.rectangle.Height / 2));

                    foreach (var crossList in this.graphProcessor.graphData.Cross)
                    {
                        if (crossList[1] == edge.EndVertex)
                        {
                            g.DrawLine(pen, edge.EndVertex.rectangle.X + (edge.EndVertex.rectangle.Width / 2),
                                      edge.EndVertex.rectangle.Y + (edge.EndVertex.rectangle.Height / 2),
                                      crossList[2].rectangle.X + (crossList[2].rectangle.Width / 2),
                                      crossList[2].rectangle.Y + (crossList[2].rectangle.Height / 2));
                        }
                    }

                }
            }
        }

        private void pridani_uzlu_Click(object sender, EventArgs e)
        {
            Vertex<string> newVertex = new Vertex<string>((this.graphProcessor.graphData.Vertices.Count + 1).ToString());
            newVertex.generateCoordinates(0, this.PaintPanel.ClientSize.Width, this.PaintPanel.ClientSize.Height);
            newVertex.rectangle = new Rectangle(newVertex.coordinateX, newVertex.coordinateY, radius, radius);
            this.graphProcessor.graphData.Vertices.Add(newVertex);
            this.PaintPanel.Invalidate();
        }

        private void vymazani_uzlu_Click(object sender, EventArgs e)
        {
            foreach (Vertex<string> item in this.graphProcessor.graphData.Vertices)
            {
                if (item.Equals(this.vertex))
                {
                    this.graphProcessor.graphData.Vertices.Remove(item);
                    this.vertex = null;
                    this.PaintPanel.Invalidate();
                    break;
                }
            }
        }

        private void posun_uzlu_Click(object sender, EventArgs e)
        {
            moove = true;
        }

        private void vytvoreni_useku_Click(object sender, EventArgs e)
        {

        }

        private void vymazani_useku_Click(object sender, EventArgs e)
        {
           

        }

    }
}