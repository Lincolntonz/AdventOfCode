using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AdventOfCode
{
    internal class _2025Day8
    {
        public string aoc2025Day8()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPathSubDirectory = Path.Combine(currentDirectory, "PuzzleInputs", "2025day8input.txt");
            string[] input = File.ReadAllLines(fullPathSubDirectory);

            // Parse vectors
            var boxLocations = input
                .Select(line =>
                {
                    var parts = line.Split(',');
                    return new Vector3(
                        float.Parse(parts[0]),
                        float.Parse(parts[1]),
                        float.Parse(parts[2]));
                })
                .ToArray();

            int n = boxLocations.Length;
            int targetConnections = 1000;

            // Union-Find (Disjoint Set)
            var uf = new UnionFind(n);

            // Precompute all pairwise distances once.
            // Use only (i < j) to avoid duplicates.
            var edges = new List<(float dist, int a, int b)>(n * (n - 1) / 2);
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    float d = Vector3.Distance(boxLocations[i], boxLocations[j]);
                    edges.Add((d, i, j));
                }
            }

            // Sort edges by distance ascending (like computing first edges of an MST)
            edges.Sort((e1, e2) => e1.dist.CompareTo(e2.dist));

            // Select first 1000 "closest unused" edges
            int connections = 0;
            foreach (var (dist, a, b) in edges)
            {
                if (connections >= targetConnections)
                    break;

                // If these two are already connected, skip
                if (uf.Find(a) == uf.Find(b))
                    continue;

                // Otherwise union them (same effect as your circuit merging logic)
                uf.Union(a, b);
                connections++;
            }

            // Extract the sizes of all connected components
            var componentSizes = uf.GetComponentSizes();

            // Multiply the top 3 largest
            long result = componentSizes
                .OrderByDescending(s => s)
                .Take(3)
                .Aggregate(1L, (acc, s) => acc * s);

            Console.WriteLine("Multiply top 3 largest circuit counts: " + result);

            return result.ToString();
        }
    }

    // -----------------------------
    // Union-Find (Disjoint Set)
    // -----------------------------
    internal class UnionFind
    {
        private readonly int[] parent;
        private readonly int[] size;

        public UnionFind(int n)
        {
            parent = new int[n];
            size = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
                size[i] = 1;
            }
        }

        public int Find(int x)
        {
            while (x != parent[x])
            {
                parent[x] = parent[parent[x]]; // path compression
                x = parent[x];
            }
            return x;
        }

        public void Union(int a, int b)
        {
            int ra = Find(a);
            int rb = Find(b);
            if (ra == rb) return;

            // union by size
            if (size[ra] < size[rb])
            {
                parent[ra] = rb;
                size[rb] += size[ra];
            }
            else
            {
                parent[rb] = ra;
                size[ra] += size[rb];
            }
        }

        public IEnumerable<int> GetComponentSizes()
        {
            // Only roots hold actual sizes
            for (int i = 0; i < parent.Length; i++)
            {
                if (parent[i] == i)
                    yield return size[i];
            }
        }
    }
}
