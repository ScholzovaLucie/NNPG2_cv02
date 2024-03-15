using cv02.Graf;
using cv02.Parser;
using cv02.Path;
using System.Collections.Generic;


namespace cv02
{
    public class GraphProcessor<T>
    {
        public Paths<T> paths { get; set; }
        public DisjunktPaths<T> DisjunktPaths { get; set; }

        public Graf<T> graphData { get; set; }
        public List<Vertex<T>> InputVertices { get; set; }
        public List<Vertex<T>> OutputVertices { get; set; }


        public void ProcessGraph(string filePath)
        {
            Parser<T> parser = new Parser<T>(filePath);
            List<Vertex<T>> vertices = parser.ExtractVertices();
            List<Edge<T>> edges = parser.ExtractEdges();
            InputVertices = parser.ExtractInputVertices();
            OutputVertices = parser.ExtractOutputVertices();
            List<List<Vertex<T>>> cross = parser.ExtractCross();

             graphData = CreateGraf(vertices, edges, cross);

            paths = new Paths<T>(graphData, InputVertices, OutputVertices);

            DisjunktPaths = new DisjunktPaths<T>(paths);

            paths.printList();
            DisjunktPaths.printList();

            Console.WriteLine("LList: "+paths.paths.Count);
            Console.WriteLine("RList: " + DisjunktPaths.getDisjonktPaths().Count);
        }

        private Graf<T> CreateGraf(List<Vertex<T>> vertices, List<Edge<T>> edges, List<List<Vertex<T>>> cross)
        {
            Graf<T> graf = new Graf<T>();

            foreach (Vertex<T> vertex in vertices)
            {
                List<Edge<T>> currentEdges = findEdges(vertex, edges);
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

        private List<Edge<T>> findEdges(Vertex<T> vertex, List<Edge<T>> edges)
        {
            List<Edge<T>> currentEdges = new List<Edge<T>>();
            foreach (var edge in edges)
            {
                if(edge.StartVertex.sameVertex(vertex)) currentEdges.Add(edge);
            }
            return currentEdges;
        }
        


    }


}
