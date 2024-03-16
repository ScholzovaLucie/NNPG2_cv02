using cv02.DrawData;
using cv02.Graf;
using cv02.Parser;
using cv02.Path;
using System.Collections.Generic;


namespace cv02
{
    public class GraphProcessor<T, TVertexData, TRdgeData>
    {
        public Paths<T, TVertexData, TRdgeData> paths { get; set; }
        public DisjunktPaths<T, TVertexData, TRdgeData> DisjunktPaths { get; set; }

        public Graf<T, TVertexData, TRdgeData> graphData { get; set; }
        public List<Vertex<T, TVertexData, TRdgeData>> InputVertices { get; set; }
        public List<Vertex<T, TVertexData, TRdgeData>> OutputVertices { get; set; }
        public List<Edge<T, TVertexData, TRdgeData>> edges { get; set; }
        public List<Vertex<T, TVertexData, TRdgeData>> vertices { get; set; }


        public void ProcessGraph(string filePath)
        {
            Parser<T, TVertexData, TRdgeData> parser = new Parser<T, TVertexData, TRdgeData>(filePath);
            vertices = parser.ExtractVertices();
            edges = parser.ExtractEdges();
            InputVertices = parser.ExtractInputVertices();
            OutputVertices = parser.ExtractOutputVertices();
            List<List<Vertex<T, TVertexData, TRdgeData>>> cross = parser.ExtractCross();

            graphData = CreateGraf(vertices, edges, cross);

            paths = new Paths<T, TVertexData, TRdgeData>(graphData, InputVertices, OutputVertices);

            DisjunktPaths = new DisjunktPaths<T, TVertexData, TRdgeData>(paths);

            paths.printList();
            DisjunktPaths.printList();

            Console.WriteLine("LList: "+paths.paths.Count);
            Console.WriteLine("RList: " + DisjunktPaths.getDisjonktPaths().Count);
        }

        private Graf<T, TVertexData, TRdgeData> CreateGraf(List<Vertex<T, TVertexData, TRdgeData>> vertices, List<Edge<T, TVertexData, TRdgeData>> edges, List<List<Vertex<T, TVertexData, TRdgeData>>> cross)
        {
            Graf<T, TVertexData, TRdgeData> graf = new Graf<T, TVertexData, TRdgeData>();

            foreach (Vertex<T, TVertexData, TRdgeData> vertex in vertices)
            {
                List<Edge<T, TVertexData, TRdgeData>> currentEdges = findEdges(vertex, edges);
                foreach (var edge in currentEdges)
                {
                    vertex.Edges.Add(edge);
                }
                graf.AddVertex(vertex);
                
            }

            foreach (var item in cross)
            {
                graf.Cross.Add(item);
            }


            return graf;
        }

        private List<Edge<T, TVertexData, TRdgeData>> findEdges(Vertex<T, TVertexData, TRdgeData> vertex, List<Edge<T, TVertexData, TRdgeData>> edges)
        {
            List<Edge<T, TVertexData, TRdgeData>> currentEdges = new List<Edge<T, TVertexData, TRdgeData>>();
            foreach (var edge in edges)
            {
                if(edge.StartVertex.sameVertex(vertex)) currentEdges.Add(edge);
            }
            return currentEdges;
        }
        


    }


}
