using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using cv02.Lists;
using Path = cv02.Lists.Path;

namespace cv02.Graf
{
    public class LList
    {
        [JsonProperty]
        public List<Path> List { get; set; }
        private int index = 1;
        private GraphData graphData;

        public LList(GraphData graphData)
        {
            List = new List<Path>();
            this.graphData = graphData;
            createLList();
        }


        private void createLList()
        {
            foreach (var startVertex in this.graphData.InputVertices)
            {
                LinkedList<Vertex> list = new LinkedList<Vertex>();
                list.AddFirst(startVertex);
                List<Vertex> visitedVertices = new List<Vertex>();
                DFS(startVertex, new Path(list), visitedVertices);
            }
        }


        public void printList()
        {
            Console.WriteLine("Dostupné cesty:");
            foreach (var path in List)
            {
                Console.WriteLine($"Cesta {path.Name}: {string.Join(" -> ", path.Vertices)}");
            }
        }

        private void DFS(Vertex currentVertex, Path currentPath, List<Vertex> visitedVertices)
        {
            IEnumerable<Edge> edges = this.graphData.Edges.Where(e => e.StartVertex.Name.Equals(currentVertex.Name));
            foreach (var edge in edges)
            {
                Vertex nextVertex = edge.EndVertex;
                
                if (!visitedVertices.Contains(nextVertex))
                {
                    var cross = isCross(currentVertex, nextVertex);
                    if (cross != null)
                    {
                        currentPath.Vertices.AddLast(nextVertex);
                        visitedVertices.Add(currentVertex);
                        nextVertex = cross;
                    }

                    if (!containsByName(nextVertex.Name, this.graphData.OutputVertices))
                    {
                        Path actualPath = currentPath.Copy();
                        actualPath.Vertices.AddLast(nextVertex);
                        visitedVertices.Add(currentVertex);
                        DFS(nextVertex, actualPath, visitedVertices);
                    }

                    lastVertex(nextVertex, currentPath, visitedVertices);

                }
            }

        }

        private Vertex isCross(Vertex current, Vertex next)
        {
            foreach (var item in this.graphData.Cross)
            {
                if (item[0].sameVertex(current) && item[1].sameVertex(next)) return item[2];
            }
            return null;
        }

        private void lastVertex(Vertex nextVertex, Path currentPath, List<Vertex> visitedVertices)
        {
            Edge exist = existNextPath(nextVertex.Name);
            if (containsByName(nextVertex.Name, this.graphData.OutputVertices) && exist != null)
            {
                currentPath.Vertices.AddLast(nextVertex);

                if (!PathAlreadyExists(currentPath))
                {
                    currentPath.setName(index++);
                    List.Add(currentPath);
                }

                Path newPath = currentPath.Copy();
                DFS(nextVertex, newPath, visitedVertices);
            }
            else if (containsByName(nextVertex.Name, this.graphData.OutputVertices))
            {
                currentPath.Vertices.AddLast(nextVertex);

                if (!PathAlreadyExists(currentPath))
                {
                    currentPath.setName(index++);
                    List.Add(currentPath);
                }

            }
        }


        private Edge findEdge(Vertex start, Vertex end)
        {
            if(start != null && end != null)
            {
            for (int i = 0; i < this.graphData.Edges.Count; i++)
            {
                if (this.graphData.Edges[i].StartVertex.sameVertex(start) && this.graphData.Edges[i].EndVertex.sameVertex(end)) return this.graphData.Edges[i]; 
            }
            }
            
            return null;
        }

     

        private bool containsByName(string name, List<Vertex> list)
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

        private Edge existNextPath(string name)
        {
            foreach (var edge in this.graphData.Edges)
            {
                if (edge.StartVertex.Name.Equals(name))
                {
                    return edge;
                }

            }
            return null;
        }

        private bool PathAlreadyExists(Path newPath)
        {
            foreach (var path in List)
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
