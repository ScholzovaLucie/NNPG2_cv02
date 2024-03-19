using cv02.DrawData;
using cv02.Graf;
using cv02.Path;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Numerics;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Path = cv02.Path;

namespace cv02
{
    public partial class Mapa : Form
    {
        private Graf<string, VertexData, EdgeData> graf = null;
        private Vertex<string, VertexData, EdgeData> vertex = null;
        private Edge<string, VertexData, EdgeData> edge = null;
        private Path<string, VertexData, EdgeData> path = null;
        private Paths<string, VertexData, EdgeData> paths;
        private DisjointPaths<string, VertexData, EdgeData> DisjointTuples = null;
        private HashSet<Path<string, VertexData, EdgeData>> disjunktPaths = null;
        private GraphProcessor<string, VertexData, EdgeData> graphProcessor = null;

        private List<Vertex<string, VertexData, EdgeData>> InputVertices { get; set; }
        private List<Vertex<string, VertexData, EdgeData>> OutputVertices { get; set; }
       
        private int radius = 20;
        private int border = 200;
        private float zoomFactor = 1.1f;
        private float previousZoomFactor = 1.0f;

        private bool dragging = false;
        private bool drawEdge = false;
        private bool drawingLine = false;
        private bool removeEdge = false;
        private bool moove = false;
        private bool removing = false;
        private bool removeVertex = false;

        Pen linePen;

        public Mapa()
        {
            InitializeComponent();
            InitializeGraphProcessor();
            InitializeEventHandlers();
            SetInitialVertexPositions();
        }

        private void InitializeGraphProcessor()
        {
            this.graphProcessor = new GraphProcessor<string, VertexData, EdgeData>();
            this.graphProcessor.ProcessGraph("../../../files/sem_01_maly.json");
            this.OutputVertices = this.graphProcessor.getOutputVertices();
            this.InputVertices = this.graphProcessor.getInputVertices();

            graf = this.graphProcessor.CreateGraf();

            calculatePaths();
        }

        private void calculatePaths()
        {
            paths = new Paths<string, VertexData, EdgeData>(this.graphProcessor, graf);
            int maxTupleSize = Math.Min(this.graphProcessor.getInputVertices().Count, this.graphProcessor.getOutputVertices().Count);
            DisjointTuples = new DisjointPaths<string, VertexData, EdgeData>(paths.paths, maxTupleSize);
            this.count_paths.Text = paths.paths.Count().ToString();
            this.DisjunktPathsCount.Text = DisjointTuples.DisjointPathSets.Count().ToString();

            this.SeznamCest.Items.Clear();
            ListViewItem[] list = new ListViewItem[paths.paths.Count];
            for (int i = 0; i < paths.paths.Count; i++)
            {
                this.SeznamCest.Items.Add(paths.paths[i].Name);
            }

            this.SeznamDisjunktnichCest.Items.Clear();
            for (int i = 0; i < DisjointTuples.DisjointPathSets.Count; i++)
            {
                String disjunkt = "";
                foreach (var item in DisjointTuples.DisjointPathSets[i])
                {
                    disjunkt += item.Name + " ";
                }
                this.SeznamDisjunktnichCest.Items.Add(disjunkt);
            }
        }

        private void InitializeEventHandlers()
        {
            this.PaintPanel.MouseDown += PaintPanel_MouseDown;
            this.PaintPanel.MouseMove += PaintPanel_MouseMove;
            this.PaintPanel.MouseUp += PaintPanel_MouseUp;
            this.PaintPanel.Paint += PaintPanel_Paint;
            this.PaintPanel.MouseWheel += PaintPanel_MouseWheel;
            this.PaintPanel.MouseClick += PaintPanel_MouseClick;
            this.SeznamCest.MouseClick += SeznamCest_ItemClick;
            this.SeznamDisjunktnichCest.MouseClick += SeznamDisjunktnichCest_ItemClick;
        }

        private void SeznamDisjunktnichCest_ItemClick(object? sender, MouseEventArgs e)
        {
            if (this.SeznamDisjunktnichCest.SelectedItem != null)
            {
                this.disjunktPaths = this.DisjointTuples.DisjointPathSets[this.SeznamDisjunktnichCest.SelectedIndex];
                this.path = null;
            }
            else
            {
                this.disjunktPaths = null;
            }

            this.PaintPanel.Invalidate();
        }

        private void SeznamCest_ItemClick(object? sender, MouseEventArgs e)
        {
            if (this.SeznamCest.SelectedItem != null)
            {
                foreach (var path in this.paths.paths)
                {
                    if (path.Name.Equals(this.SeznamCest.SelectedItem.ToString()))
                    {
                        this.path = path;
                        this.disjunktPaths = null;
                    }
                }
            }
            else
            {
                this.path = null;
            }

            this.PaintPanel.Invalidate();
        }

        private void SetInitialVertexPositions()
        {
            foreach (Vertex<string, VertexData, EdgeData> item in this.graf.Vertices)
            {
                if (this.InputVertices.Contains(item))
                {
                    item.setData(new VertexData());
                    item.data.generateCoordinates(0, border, this.PaintPanel.ClientSize.Height - radius);
                }
                else if (this.OutputVertices.Contains(item))
                {
                    item.setData(new VertexData());
                    item.data.generateCoordinates(this.PaintPanel.ClientSize.Width - radius - border, this.PaintPanel.ClientSize.Width - radius, this.PaintPanel.ClientSize.Height - radius);
                }
                else
                {
                    item.setData(new VertexData());
                    item.data.generateCoordinates(200, this.PaintPanel.ClientSize.Width - radius - border, this.PaintPanel.ClientSize.Height - radius);
                }

                item.data.rectangle = new Rectangle(item.data.coordinateX, item.data.coordinateY, radius, radius);
            }
        }

        private void PaintPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.removeEdge)
            {
                for (int i = 0; i < this.graf.Vertices.Count; i++)
                {
                    Vertex<string, VertexData, EdgeData> item = this.graf.Vertices[i];
                    for (int j = 0; j < item.Edges.Count; j++)
                    {
                        var edge = item.Edges[j];
                        double distance = DistancePointToLine(e.Location, edge.StartVertex.data.point, edge.EndVertex.data.point);
                        if (distance <= 50)
                        {
                            item.removeEdge(edge);
                            this.Invalidate();
                        }
                    }
                }
            }
        }

        private void PaintPanel_MouseDown(object sender, MouseEventArgs e)
        {
            bool found = false;

            foreach (Vertex<string, VertexData, EdgeData> item in this.graf.Vertices)
            {

                if (item.data.rectangle.Contains(e.Location))
                {
                    this.vertex = item;
                    found = true;
                    if (moove)
                    {
                        this.dragging = true;
                        this.vertex.data.point = new Point(e.X - item.data.rectangle.X, e.Y - item.data.rectangle.Y);
                    }
                    if (drawEdge)
                    {
                        this.edge = new Edge<string, VertexData, EdgeData>();
                        this.edge.Name = this.graf.Vertices.Sum(vertex => vertex.Edges.Count).ToString();
                        this.edge.StartVertex = this.vertex;
                        this.edge.EndVertex = this.vertex;
                        this.edge.setData(new EdgeData());
                        this.linePen = new Pen(Color.Black, 3);
                        this.drawingLine = true;
                        this.edge.data.startPoint = e.Location;
                        this.edge.data.endPoint = e.Location;
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

        private void PaintPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawEdge && drawingLine)
            {
                bool found = false;
                this.edge.data.endPoint = e.Location;

                foreach (Vertex<string, VertexData, EdgeData> item in this.graf.Vertices)
                {
                    if (item.data.rectangle.Contains(e.Location))
                    {
                        this.edge.EndVertex = item;
                        this.edge.StartVertex.Edges.Add(new Edge<string, VertexData, EdgeData>(edge.Name, edge.StartVertex, edge.EndVertex));
                        this.vertex = item;
                        break;
                    }
                }
            }
            if (this.removeVertex)
            {
                foreach (Vertex<string, VertexData, EdgeData> item in this.graf.Vertices)
                {
                    if (item.Equals(this.vertex))
                    {
                        this.graf.RemoveVertex(item);
                        this.vertex = null;
                        this.PaintPanel.Invalidate();
                        break;
                    }
                }
            }
            this.dragging = false;
            this.drawingLine = false;
            this.PaintPanel.Invalidate();
        }

        private void PaintPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                this.vertex.data.rectangle.Location = new Point(e.X - this.vertex.data.point.X, e.Y - this.vertex.data.point.Y);
                this.graf.UpdateVertex(this.vertex);
                this.PaintPanel.Invalidate();
            }
            if (drawEdge && drawingLine)
            {
                edge.data.endPoint = e.Location;
            }
            this.PaintPanel.Invalidate();
        }

        private void PaintPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font drawFont = new Font("Arial", 7);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();

            if (dragging && vertex != null)
            {
                g.FillEllipse(Brushes.Green, vertex.data.rectangle);

                foreach (Vertex<string, VertexData, EdgeData> item in this.graf.Vertices)
                {
                    if (!item.Equals(this.vertex))
                    {
                        g.DrawString(item.Name, drawFont, drawBrush, item.data.rectangle.X, item.data.rectangle.Y - radius, drawFormat);
                        g.FillEllipse(Brushes.Blue, item.data.rectangle);
                    }
                }
                redrawEdges(g);
            }

            calculatePaths();

            if (drawEdge && drawingLine)
            {
                g.DrawLine(linePen, edge.data.startPoint, edge.data.endPoint);
                redrawVertices(g);
                redrawEdges(g);
            }
            else
            {
                redrawVertices(g);
                redrawEdges(g);
            }

            if (this.disjunktPaths != null)
            {
                ShowDisjunkt(g);
            }

            if (this.path != null)
            {
                ShowPath(g, this.path, new Pen(Brushes.Red, 3));
            }
        }

        private void ShowDisjunkt(Graphics g)
        {
            foreach (var path in disjunktPaths)
            {
                ShowPath(g, path, new Pen(Brushes.Blue, 3));
            }
        }

        private void ShowPath(Graphics g, Path<string, VertexData, EdgeData> currentpath, Pen pen)
        {
            foreach (var item in currentpath.Vertices)
            {
                foreach (Edge<string, VertexData, EdgeData> edge in item.Edges)
                {
                    var next = currentpath.Vertices.Find(item).Next.Value;
                    if (item.Name.Equals(edge.StartVertex.Name)
                        && next.Name.Equals(edge.EndVertex.Name))
                    {
                        g.DrawLine(pen, edge.StartVertex.data.rectangle.X + (edge.StartVertex.data.rectangle.Width / 2),
                                      edge.StartVertex.data.rectangle.Y + (edge.StartVertex.data.rectangle.Height / 2),
                                      edge.EndVertex.data.rectangle.X + (edge.EndVertex.data.rectangle.Width / 2),
                                      edge.EndVertex.data.rectangle.Y + (edge.EndVertex.data.rectangle.Height / 2));

                        foreach (var crossList in this.graf.Cross)
                        {
                            if (crossList[1].Name.Equals(edge.EndVertex.Name))
                            {
                                var lastCross = currentpath.Vertices.Find(next).Next.Value;

                                if (crossList[2].Name.Equals(lastCross.Name))
                                {
                                    g.DrawLine(pen, edge.EndVertex.data.rectangle.X + (edge.EndVertex.data.rectangle.Width / 2),
                                                                             edge.EndVertex.data.rectangle.Y + (edge.EndVertex.data.rectangle.Height / 2),
                                                                             crossList[2].data.rectangle.X + (crossList[2].data.rectangle.Width / 2),
                                                                             crossList[2].data.rectangle.Y + (crossList[2].data.rectangle.Height / 2));
                                }


                            }
                        }
                    }

                }
            }
        }

        private void redrawVertices(Graphics g, int zoom = 0)
        {
            Font drawFont = new Font("Arial", 7);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();

            for (int i = 0; i < this.graf.Vertices.Count; i++)
            {
                Vertex<string, VertexData, EdgeData> item = this.graf.Vertices[i];
                item.data.rectangle.X += zoom;
                item.data.rectangle.Y += zoom;
                item.data.coordinateX += zoom;
                item.data.coordinateY += zoom;
                if (item.Equals(this.vertex))
                {
                    g.FillEllipse(Brushes.Red, item.data.rectangle);
                    g.DrawString(item.Name, drawFont, drawBrush, item.data.rectangle.X, item.data.rectangle.Y - radius, drawFormat);
                    item.data.setCoordinateX(item.data.rectangle.X);
                    item.data.setCoordinateY(item.data.rectangle.Y);
                }
                else
                {
                    if (this.InputVertices.Contains(item))
                    {
                        g.FillEllipse(Brushes.Green, item.data.rectangle);
                    }
                    else if (this.OutputVertices.Contains(item))
                    {
                        g.FillEllipse(Brushes.Yellow, item.data.rectangle);
                    }
                    else
                    {
                        g.FillEllipse(Brushes.Blue, item.data.rectangle);
                    }

                    foreach (var crossList in this.graf.Cross)
                    {
                        if (crossList[1] == item)
                        {
                            g.FillEllipse(Brushes.Purple, item.data.rectangle);
                        }
                    }

                    g.DrawString(item.Name, drawFont, drawBrush, item.data.coordinateX, item.data.coordinateY - radius, drawFormat);
                }
            }
        }

        private void redrawEdges(Graphics g)
        {
            Pen pen = new Pen(Brushes.Black, 2);
            foreach (Vertex<string, VertexData, EdgeData> v in this.graf.Vertices)
            {
                foreach (Edge<string, VertexData, EdgeData> edge in v.Edges)
                {

                    g.DrawLine(pen, edge.StartVertex.data.rectangle.X + (edge.StartVertex.data.rectangle.Width / 2),
                                      edge.StartVertex.data.rectangle.Y + (edge.StartVertex.data.rectangle.Height / 2),
                                      edge.EndVertex.data.rectangle.X + (edge.EndVertex.data.rectangle.Width / 2),
                                      edge.EndVertex.data.rectangle.Y + (edge.EndVertex.data.rectangle.Height / 2));

                    foreach (var crossList in this.graf.Cross)
                    {
                        if (crossList[1].Name.Equals(edge.EndVertex.Name))
                        {
                            g.DrawLine(pen, edge.EndVertex.data.rectangle.X + (edge.EndVertex.data.rectangle.Width / 2),
                                  edge.EndVertex.data.rectangle.Y + (edge.EndVertex.data.rectangle.Height / 2),
                                  crossList[2].data.rectangle.X + (crossList[2].data.rectangle.Width / 2),
                                  crossList[2].data.rectangle.Y + (crossList[2].data.rectangle.Height / 2));
                        }
                    }
                }
            }

        }

        private void PaintPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                if (e.Delta > 0)
                {
                    ZoomIn(e.Location);
                }
                else
                {
                    ZoomOut(e.Location);
                }
            }
        }

        private void ZoomIn(Point zoomCenter)
        {
            AdjustZoom(1.1f, zoomCenter);
        }

        private void ZoomOut(Point zoomCenter)
        {
            AdjustZoom(0.9f, zoomCenter);
        }
        private void AdjustZoom(float factor, Point zoomCenter)
        {
            previousZoomFactor = zoomFactor;
            zoomFactor *= factor;

            RepaintWithZoom(zoomCenter);
        }
        private void RepaintWithZoom(Point zoomCenter)
        {
            int newWidth = (int)(this.PaintPanel.Width * zoomFactor / previousZoomFactor);
            int newHeight = (int)(this.PaintPanel.Height * zoomFactor / previousZoomFactor);

            int offsetX = (int)((zoomCenter.X * (zoomFactor - 1)) - this.AutoScrollPosition.X);
            int offsetY = (int)((zoomCenter.Y * (zoomFactor - 1)) - this.AutoScrollPosition.Y);

            this.PaintPanel.Width = newWidth;
            this.PaintPanel.Height = newHeight;

            foreach (var vertex in this.graf.Vertices)
            {
                vertex.data.rectangle.X = (int)(vertex.data.rectangle.X * zoomFactor / previousZoomFactor);
                vertex.data.rectangle.Y = (int)(vertex.data.rectangle.Y * zoomFactor / previousZoomFactor);
                vertex.data.coordinateX = (int)(vertex.data.coordinateX * zoomFactor / previousZoomFactor);
                vertex.data.coordinateY = (int)(vertex.data.coordinateY * zoomFactor / previousZoomFactor);

            }
            this.Panel.AutoScrollMinSize = new Size(newWidth, newHeight);
            this.AutoScrollPosition = new Point(Math.Max(0, this.AutoScrollPosition.X - offsetX),
                                                 Math.Max(0, this.AutoScrollPosition.Y - offsetY));

            this.PaintPanel.Invalidate();
        }



        private void pridani_uzlu_Click(object sender, EventArgs e)
        {
            Vertex<string, VertexData, EdgeData> newVertex = new Vertex<string, VertexData, EdgeData>((this.graf.Vertices.Count + 1).ToString());
            newVertex.setData(new VertexData());
            newVertex.data.generateCoordinates(10, 10, 10);
            newVertex.data.rectangle = new Rectangle(newVertex.data.coordinateX, newVertex.data.coordinateY, radius, radius);
            this.graf.Vertices.Add(newVertex);
            this.PaintPanel.Invalidate();
        }

        private void vymazani_uzlu_Click(object sender, EventArgs e)
        {
            if (this.removeVertex)
            {
                this.vymazani_uzlu.Checked = false;
                this.removeVertex = false;
            }
            else
            {
                this.vymazani_uzlu.Checked = false;
                this.removeVertex = true;
            }

        }

        private void posun_uzlu_Click(object sender, EventArgs e)
        {
            if (this.moove)
            {
                this.posun_uzlu.Checked = false;
                this.moove = false;
            }
            else
            {
                this.posun_uzlu.Checked = true;
                this.moove = true;
            }
        }

        private void vytvoreni_useku_Click(object sender, EventArgs e)
        {
            if (this.drawEdge)
            {
                this.vytvoreni_useku.Checked = false;
                this.drawEdge = false;
            }
            else
            {
                this.vytvoreni_useku.Checked = true;
                this.drawEdge = true;
            }
        }

        private void vymazani_useku_Click(object sender, EventArgs e)
        {
            if (this.removeEdge)
            {
                this.vymazani_useku.Checked = false;
                this.removeEdge = false;
            }
            else
            {
                this.vymazani_useku.Checked = true;
                this.removeEdge = true;
            }
        }

        private double DistancePointToLine(Point point, Point lineStart, Point lineEnd)
        {
            double A = point.X - lineStart.X;
            double B = point.Y - lineStart.Y;
            double C = lineEnd.X - lineStart.X;
            double D = lineEnd.Y - lineStart.Y;

            double dot = A * C + B * D;
            double len_sq = C * C + D * D;
            double param = dot / len_sq;

            double xx, yy;

            if (param < 0)
            {
                xx = lineStart.X;
                yy = lineStart.Y;
            }
            else if (param > 1)
            {
                xx = lineEnd.X;
                yy = lineEnd.Y;
            }
            else
            {
                xx = lineStart.X + param * C;
                yy = lineStart.Y + param * D;
            }

            double dx = point.X - xx;
            double dy = point.Y - yy;

            return Math.Sqrt(dx * dx + dy * dy);
        }

    }
}
