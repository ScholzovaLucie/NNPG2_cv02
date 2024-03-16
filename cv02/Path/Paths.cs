using Newtonsoft.Json;
using cv02.Graf;

namespace cv02.Path
{
    public class Paths<T, TVertexData, TRdgeData>
    {
        [JsonProperty]
        public List<Path<T, TVertexData, TRdgeData>> paths { get; set; }
        private int index = 1;
        private Graf<T, TVertexData, TRdgeData> graphData;
        public List<Vertex<T, TVertexData, TRdgeData>> InputVertices { get; private set; }
        public List<Vertex<T, TVertexData, TRdgeData>> OutputVertices { get; private set; }

        public Paths(
            Graf<T, TVertexData, TRdgeData> graphData, 
            List<Vertex<T, TVertexData, TRdgeData>> InputVertices, 
            List<Vertex<T, TVertexData, TRdgeData>> OutputVertices
            )
        {
            this.graphData = graphData;
            this.InputVertices = InputVertices;
            this.OutputVertices = OutputVertices;
            paths = new List<Path<T, TVertexData, TRdgeData>>();
            FindPaths();
        }

        public void printList()
        {
            Console.WriteLine("Dostupné cesty:");
            foreach (var path in paths)
            {
                Console.WriteLine($"Cesta {path.Name}: {string.Join(" -> ", path.Vertices)}");
            }
        }

        public void FindPaths()
        {
            foreach (var inputVertex in InputVertices)
            {
                List<Vertex<T, TVertexData, TRdgeData>> visited = new List<Vertex<T, TVertexData, TRdgeData>>();
                DFS(inputVertex, visited);
            }
        }

        private void DFS(Vertex<T, TVertexData, TRdgeData> currentVertex, List<Vertex<T, TVertexData, TRdgeData>> visited)
        {
            visited.Add(currentVertex);


            if (
                containsByName(currentVertex.Name, OutputVertices) &&
                visited.Count > 1 &&
                !PathAlreadyExists(visited)
                )
            {
                paths.Add(new Path<T, TVertexData, TRdgeData>(index++, new LinkedList<Vertex<T, TVertexData, TRdgeData>>(visited)));
            }

            foreach (var edge in currentVertex.Edges)
            {
                if (!visited.Contains(edge.EndVertex))
                {
                    DFS(edge.EndVertex, visited);
                }
            }


            foreach (var crossList in graphData.Cross)
            {
                if (crossList[0] == currentVertex)
                {
                    visited.Add(crossList[1]);
                    DFS(crossList[2], visited);
                }
            }

            visited.Remove(currentVertex);
        }


        private bool containsByName(T name, List<Vertex<T, TVertexData, TRdgeData>> list)
        {
            foreach (var vertex in list)
            {
                if (vertex.Name.Equals(name))
                {
                    return true;
                }
            }

            return false;
        }

        private bool PathAlreadyExists(List<Vertex<T, TVertexData, TRdgeData>> newPath)
        {
            foreach (var path in paths)
            {
                if (path.Equals(newPath))
                {
                    return true;
                }
            }
            return false;
        }
    }
}