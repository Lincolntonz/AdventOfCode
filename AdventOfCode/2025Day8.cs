using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AdventOfCode
{
    internal class _2025Day8
    {
        public string aoc2025Day8()
        {
            var result = 0.ToString();

            // Get the current working directory
            string currentDirectory = Directory.GetCurrentDirectory();
            // Relative path to a file in a subdirectory
            string subDirectoryFile = Path.Combine("PuzzleInputs", "2025day8input.txt");
            string fullPathSubDirectory = Path.Combine(currentDirectory, subDirectoryFile);
            string[] input = System.IO.File.ReadAllLines(fullPathSubDirectory);

            //parse each box into its on vector with x,y,z
            List<Vector3> boxLocations = new List<Vector3>();
            foreach (string line in input)
            {
                string[] coords = line.Split(',');
                boxLocations.Add(new Vector3(float.Parse(coords[0]), float.Parse(coords[1]), float.Parse(coords[2])));
                //Console.WriteLine(String.Format(@"{0},{1},{2}", boxLocations.Last().X, boxLocations.Last().Y, boxLocations.Last().Z));
            }

            //each circuit is a list of connected boxes.
            List<List<Vector3>> circuits = new List<List<Vector3>>();

            //for each connection (#connections)
            int connections = 10;
            List<int[]> matchedBoxes = new List<int[]>();
            while (connections-- > 0)
            {
                //finds the next closest pair ( that hasn't already been matched)
                int[] closestPair = FindNextClosestPair(input, boxLocations, matchedBoxes);
                /// check to see if either of this pair are already contained in any circuit.
                //check to see if these two boxes are already connected (in same circuit)
                //if not, add them to their own circuit.

            }

            Vector3 vector3 = new Vector3(0, 1, 2);
            //Console.WriteLine(String.Format(@"Vector X: {0} Y: {1} Z: {2}", vector3.X, vector3.Y, vector3.Z)); 
            return result;

        }

        private static int[] FindNextClosestPair(string[] input, List<Vector3> boxLocations, List<int[]> matchedBoxes)
        {
            int[] closestPair = new int[2];
            float shortestDistance = float.MaxValue;
            //find two boxes closest to each other, straight line distance.
            for (int box1 = 0; box1 < input.Length; box1++)
            {
                for (int box2 = 0; box2 < input.Length; box2++)
                {
                    //don't compare box to itself, or do a match we've already done.
                    if (box1 != box2 && !(matchedBoxes.Any(array => array.SequenceEqual(new int[] { box1, box2 }))))
                    {
                        var thisDistance = Vector3.Distance(boxLocations[box1], boxLocations[box2]);
                        //this distance is the shortest so far.
                        if (thisDistance < shortestDistance)
                        {
                            shortestDistance = thisDistance;
                            closestPair[0] = box1;
                            closestPair[1] = box2;

                        }
                    }
                }

            }
            //next time, ignore this this as the shortest distance.
            //rather than removing from the input as these points can match with different points.
            matchedBoxes.Add(closestPair);
            matchedBoxes.Add(new int[] { closestPair[1], closestPair[0] });
            Console.WriteLine("Closest Pair: " + boxLocations[closestPair[0]].ToString() + " " + boxLocations[closestPair[1]].ToString());

            return closestPair;
        }
    }

}
