using cv02.DrawData;
using cv02.Graf;
using cv02.Path;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Numerics;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using Path = cv02.Path;

namespace cv02
{
    public partial class Mapa : Form
    {
        private GraphProcessor<string, VertexData, EdgeData> graphProcessor { get; set; }
        private Vertex<string, VertexData, EdgeData> vertex = null;
        private Edge<string, VertexData, EdgeData> edge = null;
        private bool dragging = false;
        private bool drawEdge = false;
        private bool drawingLine = false;
        private bool removeEdge = false;
        private bool moove = false;
        private bool removing = false;
        private int radius = 20;
        private int border = 200;
        private float zoomFactor = 1.1f;
        private float previousZoomFactor = 1.0f;
        Pen linePen;

        public Mapa()
        {
            // Inicializace grafického procesoru a nastavení událostí panelu pro kreslení
            InitializeComponent();
            InitializeGraphProcessor();
            InitializeEventHandlers();
            SetInitialVertexPositions();
        }

        private void InitializeGraphProcessor()
        {
            // Inicializace grafického procesoru a zpracování grafu ze souboru
            this.graphProcessor = new GraphProcessor<string, VertexData, EdgeData>();
            this.graphProcessor.ProcessGraph("../../../files/sem_01_maly.json");
            this.count_paths.Text = this.graphProcessor.paths.paths.Count().ToString();
            this.DisjunktPathsCount.Text = this.graphProcessor.DisjunktPaths.disjointPaths.Count().ToString();
        }

        private void InitializeEventHandlers()
        {
            // Pøiøazení událostí pro myš
            this.PaintPanel.MouseDown += PaintPanel_MouseDown;
            this.PaintPanel.MouseMove += PaintPanel_MouseMove;
            this.PaintPanel.MouseUp += PaintPanel_MouseUp;
            this.PaintPanel.Paint += PaintPanel_Paint;
            this.PaintPanel.MouseWheel += PaintPanel_MouseWheel;
        }

        private void SetInitialVertexPositions()
        {
            // Nastavení poèáteèních pozic vrcholù
            foreach (Vertex<string, VertexData, EdgeData> item in this.graphProcessor.graphData.Vertices)
            {
                if (this.graphProcessor.InputVertices.Contains(item))
                {
                    item.setData(new VertexData());
                    item.data.generateCoordinates(0, border, this.PaintPanel.ClientSize.Height - radius);
                }
                else if (this.graphProcessor.OutputVertices.Contains(item))
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

        private void PaintPanel_MouseDown(object sender, MouseEventArgs e)
        {
            bool found = false;

            foreach (Vertex<string, VertexData, EdgeData> item in this.graphProcessor.graphData.Vertices)
            {
                
                if (item.data.rectangle.Contains(e.Location))
                {
                    this.vertex = item;
                    found = true;
                    if (this.removeEdge)
                    {
                        this.removing = true;
                        this.edge = new Edge<string, VertexData, EdgeData>();
                        this.edge.Name = this.graphProcessor.edges.Count.ToString();
                        this.edge.StartVertex = this.vertex;
                        this.edge.EndVertex = this.vertex;
                        this.edge.setData(new EdgeData());
                    }
                    if (moove)
                    {
                        this.dragging = true;
                        this.vertex.data.point = new Point(e.X - item.data.rectangle.X, e.Y - item.data.rectangle.Y);
                    }
                    if (drawEdge)
                    {
                        this.edge = new Edge<string, VertexData, EdgeData>();
                        this.edge.Name = this.graphProcessor.edges.Count.ToString();
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

                foreach (Vertex<string, VertexData, EdgeData> item in this.graphProcessor.graphData.Vertices)
                {
                    if (item.data.rectangle.Contains(e.Location))
                    {
                        this.edge.EndVertex = item;
                        this.vertex = item;
                        item.Edges.Add(edge);
                        found = true;
                    }
                }

                if (found)
                {
                    this.graphProcessor.edges.Add(edge);
                }
            }

            if(this.removing && this.removeEdge)
            {
                bool found = false;
                Edge<string, VertexData, EdgeData> deleteEdge = new Edge<string, VertexData, EdgeData>();
                foreach (Vertex<string, VertexData, EdgeData> item in this.graphProcessor.graphData.Vertices)
                {
                    if (item.data.rectangle.Contains(e.Location))
                    {
                        this.edge.EndVertex = item;
                        this.vertex = item;
                        

                        foreach (var actualEdge in this.graphProcessor.edges)
                        {
                            if (actualEdge.EndVertex.Name.Equals(edge.EndVertex.Name) && actualEdge.StartVertex.Name.Equals(edge.StartVertex.Name))
                            {
                                deleteEdge = actualEdge;
                            }
                        }
                        edge.StartVertex.removeEdge(deleteEdge);
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    this.graphProcessor.edges.Remove(deleteEdge);
                }
            }
            this.removing = false;
            this.moove = false;
            this.dragging = false;
            this.drawingLine = false;
            this.drawEdge = false;
            this.PaintPanel.Invalidate();
        }

        private void PaintPanel_MouseMove(object sender, MouseEventArgs e)
        {
            bool foundMagnetic = false;
            if (dragging)
            {
                this.vertex.data.rectangle.Location = new Point(e.X - this.vertex.data.point.X, e.Y - this.vertex.data.point.Y);
                this.graphProcessor.graphData.UpdateVertex(this.vertex);
                this.PaintPanel.Invalidate();
            }
            if (drawEdge && drawingLine)
            {
                foreach (Vertex<string, VertexData, EdgeData> item in this.graphProcessor.graphData.Vertices)
                {
                    foundMagnetic = true;
                    Console.WriteLine(item.data.magneticRectangle.X + ": " + item.data.magneticRectangle.Y);
                }

                edge.data.endPoint = e.Location;
            }
            this.PaintPanel.Invalidate();
        }

        private void PaintPanel_Paint(object sender, PaintEventArgs e)
        {
            // Provádìt kreslení v reakci na událost Paint
            Graphics g = e.Graphics;
            Font drawFont = new Font("Arial", 7);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();

            // Pokud je zapnuté pøetahování, vykreslíme objekt zelenì
            if (dragging && vertex != null)
            {
                g.FillEllipse(Brushes.Green, vertex.data.rectangle);

                foreach (Vertex<string, VertexData, EdgeData> item in this.graphProcessor.graphData.Vertices)
                {
                    if (!item.Equals(this.vertex))
                    {
                        g.DrawString(item.Name, drawFont, drawBrush, item.data.rectangle.X, item.data.rectangle.Y - radius, drawFormat);
                        g.FillEllipse(Brushes.Blue, item.data.rectangle);
                    }
                }
                redrawEdges(g);
            }
            // Pokud je zapnuté kreslení úseèky, vykreslíme úseèku èernì
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
        }

        private void redrawVertices(Graphics g, int zoom = 0)
        {
            Font drawFont = new Font("Arial", 7);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();

            for (int i = 0; i < this.graphProcessor.graphData.Vertices.Count; i++)
            {
                Vertex<string, VertexData, EdgeData> item = this.graphProcessor.graphData.Vertices[i];
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
                    if (this.graphProcessor.InputVertices.Contains(item))
                    {
                        g.FillEllipse(Brushes.Green, item.data.rectangle);
                    }
                    else if (this.graphProcessor.OutputVertices.Contains(item))
                    {
                        g.FillEllipse(Brushes.Yellow, item.data.rectangle);
                    }
                    else
                    {
                        g.FillEllipse(Brushes.Blue, item.data.rectangle);
                    }

                    foreach (var crossList in this.graphProcessor.graphData.Cross)
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

            foreach (Vertex<string, VertexData, EdgeData> v in this.graphProcessor.graphData.Vertices)
            {
                foreach (Edge<string, VertexData, EdgeData> edge in v.Edges)
                {
                    g.DrawLine(pen, edge.StartVertex.data.rectangle.X + (edge.StartVertex.data.rectangle.Width / 2),
                                      edge.StartVertex.data.rectangle.Y + (edge.StartVertex.data.rectangle.Height / 2),
                                      edge.EndVertex.data.rectangle.X + (edge.EndVertex.data.rectangle.Width / 2),
                                      edge.EndVertex.data.rectangle.Y + (edge.EndVertex.data.rectangle.Height / 2));

                    foreach (var crossList in this.graphProcessor.graphData.Cross)
                    {
                        if (crossList[1] == edge.EndVertex)
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
            AdjustZoom(1.1f, zoomCenter); // Zoom in by 10%
        }

        private void ZoomOut(Point zoomCenter)
        {
            AdjustZoom(0.9f, zoomCenter); // Zoom out by 10%
        }
        private void AdjustZoom(float factor, Point zoomCenter)
        {
            previousZoomFactor = zoomFactor;
            zoomFactor *= factor;

           

            // Adjust edges if needed similarly

            RepaintWithZoom(zoomCenter);
        }



        private void RepaintWithZoom(Point zoomCenter)
        {
            // Update PaintPanel size

            int newWidth = (int)(this.PaintPanel.Width * zoomFactor / previousZoomFactor);
            int newHeight = (int)(this.PaintPanel.Height * zoomFactor / previousZoomFactor);

            int offsetX = (int)((zoomCenter.X * (zoomFactor - 1)) - this.AutoScrollPosition.X);
            int offsetY = (int)((zoomCenter.Y * (zoomFactor - 1)) - this.AutoScrollPosition.Y);

            this.PaintPanel.Width = newWidth;
            this.PaintPanel.Height = newHeight;

            foreach (var vertex in this.graphProcessor.graphData.Vertices)
            {
                vertex.data.rectangle.X = (int)(vertex.data.rectangle.X * zoomFactor / previousZoomFactor);
                vertex.data.rectangle.Y = (int)(vertex.data.rectangle.Y * zoomFactor / previousZoomFactor);
                vertex.data.coordinateX = (int)(vertex.data.coordinateX * zoomFactor / previousZoomFactor);
                vertex.data.coordinateY = (int)(vertex.data.coordinateY * zoomFactor / previousZoomFactor);

            }
            this.Panel.AutoScrollMinSize = new Size(newWidth, newHeight);
            this.AutoScrollPosition = new Point(Math.Max(0, this.AutoScrollPosition.X - offsetX),
                                                 Math.Max(0, this.AutoScrollPosition.Y - offsetY));

            this.PaintPanel.Invalidate(); // Trigger a repaint
        }

       

        private void pridani_uzlu_Click(object sender, EventArgs e)
        {
            VertexData vertexData = new VertexData();
            Vertex<string, VertexData, EdgeData> newVertex = new Vertex<string, VertexData, EdgeData>((this.graphProcessor.graphData.Vertices.Count + 1, vertexData).ToString());
            newVertex.setData(new VertexData());
            newVertex.data.generateCoordinates(0, this.PaintPanel.ClientSize.Width, this.PaintPanel.ClientSize.Height);
            newVertex.data.rectangle = new Rectangle(newVertex.data.coordinateX, newVertex.data.coordinateY, radius, radius);
            this.graphProcessor.graphData.Vertices.Add(newVertex);
            this.PaintPanel.Invalidate();
        }

        private void vymazani_uzlu_Click(object sender, EventArgs e)
        {
            foreach (Vertex<string, VertexData, EdgeData> item in this.graphProcessor.graphData.Vertices)
            {
                if (item.Equals(this.vertex))
                {
                    this.graphProcessor.graphData.RemoveVertex(item);
                    this.vertex = null;
                    this.PaintPanel.Invalidate();
                    break;
                }
            }
        }

        private void posun_uzlu_Click(object sender, EventArgs e)
        {
            this.moove = true;
        }

        private void vytvoreni_useku_Click(object sender, EventArgs e)
        {
            this.drawEdge = true;
        }

        private void vymazani_useku_Click(object sender, EventArgs e)
        {
            this.removeEdge = true;
        }
    }
}
