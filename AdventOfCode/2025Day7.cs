using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class _2025Day7
    {

        public string aoc2025Day7()
        {
            var resultString = "0";
            //string inputPath = "C:\\CURRENT\\Testbed\\AdventOfCode\\AdventOfCode\\PuzzleInputs\\2025day7input.txt";
            //string[] inputLines = System.IO.File.ReadAllLines(inputPath);
            // Get the current working directory
            string currentDirectory = Directory.GetCurrentDirectory();
            // Relative path to a file in a subdirectory
            string subDirectoryFile = Path.Combine("PuzzleInputs", "2025day7input.txt");
            string fullPathSubDirectory = Path.Combine(currentDirectory, subDirectoryFile);
            string[] input = System.IO.File.ReadAllLines(fullPathSubDirectory);
            ///could also go through each line, string.Split(^)

            //grid will be maniuplated for tachyon path
            string[] tachyonGrid = input;

            long beamSplit = 0;

            int[] startingIndex = new int[] { 0, input[0].IndexOf('S') };  ///starting point will be 'S' on first line.
            //Console.WriteLine("starting index: " + startingIndex);
            //test input read


            //foreach (string str in input)
            //    Console.WriteLine(str);
            Console.WriteLine(RunManifold(input).timelines.ToString());

            //beamSplit = Part2(input, tachyonGrid, beamSplit, startingIndex);


            return beamSplit.ToString();
        }

        private static long Part1(string[] input, string[] tachyonGrid, long beamSplit, int[] startingIndex)
        {

            List<int> beamsOnPreviousRow = new List<int>(); //index value of each beam on previousRow
            List<int> beamsOnthisRow = new List<int>(); //index of each beam on previousRow
            List<int> beamsOnNextRow = new List<int>();

            beamsOnPreviousRow.Add(startingIndex[1]);
            for (int i = 1; i < tachyonGrid.Length; i++)  //i=1, don't start on entry point row.
            {

                foreach (int incomingBeamColumn in beamsOnPreviousRow)
                {

                    //beam is incoming at this index.
                    //on this row, check to see if its a splitter (^) or empty space (.)
                    char hit = input[i][incomingBeamColumn];
                    switch (hit)
                    {
                        case '^':
                            //beam splits. Beam will be on incomingBeamColumn-1 and incomingBeamColumn+1. check bounds 
                            beamsOnNextRow.Add(incomingBeamColumn - 1 < 0 ? 0 : incomingBeamColumn - 1);//cannot be less than 0.
                            tachyonGrid[i] = ReplaceAt(tachyonGrid[i], beamsOnNextRow.Last(), '|');
                            //beam to the right index cannot be greater than the max length of the line.
                            beamsOnNextRow.Add(incomingBeamColumn + 1 > tachyonGrid[0].Length - 1 ? tachyonGrid[0].Length - 1 : incomingBeamColumn + 1);
                            tachyonGrid[i] = ReplaceAt(tachyonGrid[i], beamsOnNextRow.Last(), '|');
                            beamSplit++;
                            break;
                        case '.':
                            //empty space. beam will be here. 
                            //string immutable, removeat and ad to get around this
                            tachyonGrid[i] = ReplaceAt(tachyonGrid[i], incomingBeamColumn, '|');
                            beamsOnNextRow.Add(incomingBeamColumn);
                            break;
                    }

                }

                beamsOnPreviousRow.Clear();
                beamsOnPreviousRow.AddRange(beamsOnNextRow);
                beamsOnNextRow.Clear();

                Console.WriteLine(tachyonGrid[i]);

            }
            Console.Write("Total number of beam splits: " + beamSplit);
            return beamSplit;
        }
        private static long Part2(string[] input, string[] tachyonGrid, long beamSplit, int[] startingIndex)
        {
            ///For part 2, beam will only take one path when hitting a spliter.
            ///Answer is the number of different paths a beam could take to reach the bottom.
            /// count paths off of a split. Count '|' , but do not count an '|' that follows another '|'.

            //pretty much had it, need to re-write using long[width] arrays instead of list<ints>.
            //then instead of carrying just a beam flag that beam is incoming, carry the count that path has been taken (if it's new -- previous>0)

            List<int> beamsOnPreviousRow = new List<int>(); //index value of each beam on previousRow
            List<int> beamsOnthisRow = new List<int>(); //index of each beam on previousRow
            List<int> beamsOnNextRow = new List<int>();
            long beamPossibility = 0;

            long countPipes = 0;
            long countPipes2 = 0;
            //add starting point beam.
            beamsOnPreviousRow.Add(startingIndex[1]);
            //int[] pathCounter = new int[input[0].Length];
            Dictionary<int, int> pathCounter = new Dictionary<int, int>();
            for (int i = 0; i < input[0].Length; i++)
            {
                if (i == startingIndex[1])
                {
                    pathCounter.Add(i, 1);

                }
                else 
                    pathCounter.Add(i, 0);
            }


            for (int i = 1; i < tachyonGrid.Length; i++)  //i=1, don't start on entry point row.
            {

                foreach (int incomingBeamColumn in beamsOnPreviousRow)
                {

                    //beam is incoming at this index.
                    //on this row, check to see if its a splitter (^) or empty space (.)
                    char hit = input[i][incomingBeamColumn];
                    switch (hit)
                    {
                        case '^':
                            //beam splits. Beam will be on incomingBeamColumn-1 and incomingBeamColumn+1. check bounds 
                            beamsOnNextRow.Add(incomingBeamColumn - 1 < 0 ? 0 : incomingBeamColumn - 1);//cannot be less than 0.
                            tachyonGrid[i] = ReplaceAt(tachyonGrid[i], beamsOnNextRow.Last(), '|');
                            //beam to the right index cannot be greater than the max length of the line.
                            beamsOnNextRow.Add(incomingBeamColumn + 1 > tachyonGrid[0].Length - 1 ? tachyonGrid[0].Length - 1 : incomingBeamColumn + 1);
                            tachyonGrid[i] = ReplaceAt(tachyonGrid[i], beamsOnNextRow.Last(), '|');
                            beamSplit++;
                            pathCounter[incomingBeamColumn - 1] += beamsOnPreviousRow[i];
                            pathCounter[incomingBeamColumn + 1] += beamsOnPreviousRow[i];
                            break;
                        case '.':
                            //empty space. beam will be here. 
                            //string immutable, removeat and ad to get around this
                            tachyonGrid[i] = ReplaceAt(tachyonGrid[i], incomingBeamColumn, '|');
                            beamsOnNextRow.Add(incomingBeamColumn);
                            pathCounter[incomingBeamColumn + 1] += beamsOnPreviousRow[i];

                            break;
                    }
                }
                foreach (KeyValuePair<int, int> column in pathCounter)
                {
                    //countPipes2 += column.Value;
                    Console.Write(column.Value + " ");
                }

                //need to keep track of number of times a path was taken. this would mean we need an int[tachyongrid[0].length], where each index is a counter for how many times the path occurs.
                foreach (int beamlocation in beamsOnNextRow)
                {
                    //pathCounter[beamlocation]++; 
                }


                beamsOnPreviousRow.Clear();
                beamsOnPreviousRow.AddRange(beamsOnNextRow);
                beamsOnNextRow.Clear();

                Console.WriteLine(tachyonGrid[i]);

            }
            foreach (KeyValuePair<int,int> column in pathCounter)
            {
                countPipes2 += column.Value;
                Console.Write("Column: " + column.Key + " - " + column.Value + "\n");
            }


            //var countpipes2 = 0;
            ////count pipes.
            //foreach (string str in tachyonGrid)
            //{
            //    foreach (char c in str)
            //    {
            //        if (c == '|')
            //            countpipes2++;
            //    }
            //}
            //foreach (int p in pathCounter)
            //{
            //    countPipes2 += p;
            //}

            Console.Write(String.Format(@"Count splits total: {0} Count pipes: {1} ", beamSplit, countPipes2));
            return beamSplit;
        }



        public static string ReplaceAt(string input, int index, char newChar)
        {
            if (input == null) { throw new ArgumentNullException("input"); }
            char[] chars = input.ToCharArray();// string to char array
            chars[index] = newChar; //replace index with newCHar
            return new string(chars); //put back to string and return.
        }
        /// <summary>
        /// https://github.com/andrewscodedump/Advent/blob/master/Advent/2025/Done/Days07-12/Day07.cs
        /// pulled from solution thread.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public (int splits, long timelines) RunManifold(string[] input)
        {
            // Dynamic programming over the grid:
            //
            // Each cell in row i depends only on the values from row i-1 directly above it.
            // We propagate a vector of "timeline counts" downward row by row instead of
            // keeping a full 2D DP table, which reduces memory to O(columns).
            //
            // At forks ('^'), a timeline splits into left/right branches, and we count a
            // "split" whenever an active timeline actually forks (>0 incoming paths).

            var lines = input;
            var crow = lines.Length;
            var ccol = lines[0].Length;
            var splits = 0;
            var timelines = new long[ccol];

            for (int irow = 0; irow < crow; irow++)
            {
                var nextTimelines = new long[ccol];
                for (var icol = 0; icol < ccol; icol++)
                {
                    if (lines[irow][icol] == 'S')
                    {
                        nextTimelines[icol] = 1;
                    }
                    else if (lines[irow][icol] == '^')
                    {
                        splits += timelines[icol] > 0 ? 1 : 0;
                        nextTimelines[icol - 1] += timelines[icol];
                        nextTimelines[icol + 1] += timelines[icol];
                    }
                    else
                    {
                        nextTimelines[icol] += timelines[icol];
                    }
                }
                foreach (long i in timelines)
                {
                    //countPipes2 += column.Value;
                    Console.Write(i == 0 ? ".":i.ToString());
                }
                Console.WriteLine();

                timelines = nextTimelines;
            }
            return (splits, timelines.Sum());
        }
    }
}
