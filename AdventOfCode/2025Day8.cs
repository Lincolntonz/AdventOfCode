using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class _2025Day8
    {
        /// <summary>
        /// This worked for part 1, but took like three hours.
        /// </summary>
        /// <returns></returns>
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
            int connections = 1000;
            List<int[]> matchedBoxes = new List<int[]>();
            while (connections-- > 0)
            {
                //finds the next closest pair ( that hasn't already been matched)
                int[] closestPair = FindNextClosestPair(input, boxLocations, matchedBoxes);

                /// check to see if either of this pair are already contained in any circuit.
                /// 
                Vector3 box1 = boxLocations[closestPair[0]];
                Vector3 box2 = boxLocations[closestPair[1]];

                //returns the circuit the box is in. Otherwise, returns null.
                List<Vector3> box1Circuit = FindContainingListManual(circuits, box1);
                List<Vector3> box2Circuit = FindContainingListManual(circuits, box2);
                
                //four possibilites:
                if (box1Circuit == box2Circuit && (box1Circuit!=null))
                {
                    //already in same circuit. do nothing.
                    
                    continue;
                }
                else if ((box1Circuit != null) && (box2Circuit == null))
                {
                    //box 1 is in a circuit, box 2 is NOT.
                    //add box2 to box1's circuit
                    circuits[circuits.IndexOf(box1Circuit)].Add(box2);

                    //if box2 IS in in another circuit (othercirc)  othercirc and circuit become one. Move all elements to othercirc and remove circuit from circuits.
                    
                    continue; ;
                }
                else if ((box1Circuit == null) && (box2Circuit != null))
                {
                    //box 1 is NOT in a circuit, box 2 is .
                    //add box2 to box1's circuit
                    circuits[circuits.IndexOf(box2Circuit)].Add(box1);

                    continue; 
                }
                else if ((box1Circuit != null) && (box2Circuit != null))
                {
                    //both are in a circuit already. Add all boxes from boxCircuit2 to boxCircuit1. Remove boxCircuit2.
                    var box1CircuitIndex = circuits.IndexOf(box1Circuit);
                    var box2CircuitIndex = circuits.IndexOf(box2Circuit);
                    circuits[box1CircuitIndex].AddRange(box2Circuit);
                    circuits.RemoveAt(box2CircuitIndex);

                    continue;

                }
                else if ((box1Circuit == null) && (box2Circuit == null))
                {
                    //neither is in any circuit. 
                    circuits.Add(new List<Vector3> { box1, box2 });
                    continue;
                }


            }

            foreach (var circuit in circuits)
            {
                Console.WriteLine(circuit.Count());

            }
            result = MultiplyTopNCounts(circuits, 3).ToString();
            Console.WriteLine("MUltiply top 3 largest circuit coutns: " + result);  
            //Console.WriteLine(String.Format(@"Vector X: {0} Y: {1} Z: {2}", vector3.X, vector3.Y, vector3.Z)); 
            return result;

        }

        public static List<T> FindContainingListManual<T>(List<List<T>> listOfLists, T elementToFind)
        {
            foreach (var innerList in listOfLists)
            {
                if (innerList.Contains(elementToFind))
                {
                    return innerList;
                }
            }

            return null; // Return null if the element is not found in any inner list
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
           // Console.WriteLine("Closest Pair: " + boxLocations[closestPair[0]].ToString() + " " + boxLocations[closestPair[1]].ToString());

            return closestPair;
        }


        /// <summary>
        /// CoPiloted this one ,, just need to multiply the top 3 largest circuits Counts. Didn't feel like doing it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="circuits"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /* 
        Pseudocode / Plan (detailed):
        1. Validate inputs:
           - If `circuits` is null -> throw ArgumentNullException.
           - If `n` <= 0 -> throw ArgumentOutOfRangeException (caller should request positive n).
        2. Compute counts for each inner list:
           - For each inner list in `circuits`, take its `Count`. If an inner list is null treat its count as 0.
        3. Sort counts in descending order.
        4. Take the top `n` counts (if there are fewer than `n` lists, take what's available).
        5. If no counts were taken (empty result), return 0 to indicate no productable values.
        6. Multiply the taken counts into a `long` product:
           - Start product = 1.
           - For each count, multiply product *= count.
           - (No special overflow handling here; let CLR overflow behavior apply or wrap in checked by caller.)
        7. Return the product.

        Behavior decisions:
        - `n` must be positive; invalid callers should be notified via exception.
        - If `circuits` contains fewer than `n` lists, we multiply the available top counts.
        - If none available, return 0.
        */

        public static long MultiplyTopNCounts<T>(List<List<T>> circuits, int n)
        {
            if (circuits is null)
            {
                throw new ArgumentNullException(nameof(circuits));
            }

            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "n must be a positive integer.");
            }

            // Get counts (treat null inner lists as count 0), sort desc, take top n
            var topCounts = circuits
                .Select(inner => inner?.Count ?? 0)
                .OrderByDescending(c => c)
                .Take(n)
                .ToArray();

            if (topCounts.Length == 0)
            {
                return 0;
            }

            long product = 1;
            foreach (var count in topCounts)
            {
                product *= count;
            }

            return product;
        }
    }

}
