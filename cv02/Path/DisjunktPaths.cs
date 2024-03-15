using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using cv02.Graf;

namespace cv02.Path
{
    public class DisjunktPaths<T>
    {
        [JsonProperty]
        public List<HashSet<Path<T>>> disjointPaths { get; set; }

        public DisjunktPaths(Paths<T> LList)
        {
            disjointPaths = new List<HashSet<Path<T>>>();
            FindDisjointPaths(LList.paths, new HashSet<Path<T>>());
        }

        public List<HashSet<Path<T>>> getDisjonktPaths()
        {
            return disjointPaths;
        }

        public void printList()
        {
            Console.WriteLine("Disjunktní množina cest: ");
            foreach (var disjointSet in disjointPaths)
            {
                foreach (var path in disjointSet)
                {
                    Console.Write(path.Name + ", ");
                }
                Console.WriteLine();
            }
        }

        private void FindDisjointPaths(List<Path<T>> paths, HashSet<Path<T>> currentSet)
        {

            foreach (var path_first in paths)
            {
                var filtered = paths.Where(p => !p.getFirst().Name.Equals(path_first.getFirst().Name)).ToList();
                filtered = filtered.Where(p => !p.getLast().Name.Equals(path_first.getLast().Name)).ToList();

                foreach (var path_second in filtered)
                {
                    currentSet.Add(path_first);
                    currentSet.Add(path_second);

                    if (path_first.IsDisjoint(path_second) && !disjunktAlreadyExist(currentSet))
                    {
                        disjointPaths.Add(new HashSet<Path<T>>(currentSet));
                        FindDisjunktPathsToSet(paths, currentSet);
                    }
                    currentSet.Clear();

                }

            }

        }

        private void FindDisjunktPathsToSet(List<Path<T>> paths, HashSet<Path<T>> currentSet)
        {
            int count = 0;

            var filtered = paths;

            foreach (var path_first in currentSet)
            {
                filtered = filtered.Where(p => !p.getFirst().Name.Equals(path_first.getFirst().Name)).ToList();
                filtered = filtered.Where(p => !p.getLast().Name.Equals(path_first.getLast().Name)).ToList();
            }

            foreach (var path_second in filtered)
            {
                if (IsDisjointSet(currentSet, path_second))
                {
                    currentSet.Add(path_second);

                    if (!disjunktAlreadyExist(currentSet))
                    {
                        disjointPaths.Add(new HashSet<Path<T>>(currentSet));
                        FindDisjunktPathsToSet(paths, currentSet);
                    }
                    else
                    {
                        currentSet.Remove(path_second);
                    }

                }
            }


        }

        private bool disjunktAlreadyExist(HashSet<Path<T>> set)
        {
            if (disjointPaths.Count == 0) return false;

            foreach (var path in disjointPaths)
            {
                if (path.SetEquals(set))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsDisjointSet(HashSet<Path<T>> set, Path<T> newPath)
        {
            foreach (var path in set)
            {
                if (!path.IsDisjoint(newPath))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
