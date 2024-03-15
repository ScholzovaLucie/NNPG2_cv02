using Newtonsoft.Json;
using cv02.Graf;

namespace cv02.Path
{
    public class Paths<T>
    {
        [JsonProperty]
        public List<Path<T>> paths { get; set; }
        private int index = 1;
        private Graf<T> graphData;
        public List<Vertex<T>> InputVertices { get; private set; }
        public List<Vertex<T>> OutputVertices { get; private set; }

        public Paths(Graf<T> graphData, List<Vertex<T>> InputVertices, List<Vertex<T>> OutputVertices)
        {
            this.graphData = graphData;
            this.InputVertices = InputVertices;
            this.OutputVertices = OutputVertices;
            paths = new List<Path<T>>();
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
                List<Vertex<T>> visited = new List<Vertex<T>>();
                DFS(inputVertex, visited);
            }
        }

        private void DFS(Vertex<T> currentVertex, List<Vertex<T>> visited)
        {
            visited.Add(currentVertex);


            if (
                containsByName(currentVertex.Name, OutputVertices) &&
                visited.Count > 1 &&
                !PathAlreadyExists(visited)
                )
            {
                paths.Add(new Path<T>(index++, new LinkedList<Vertex<T>>(visited)));
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


        private bool containsByName(T name, List<Vertex<T>> list)
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

        private bool PathAlreadyExists(List<Vertex<T>> newPath)
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