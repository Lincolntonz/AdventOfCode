using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class _2025Day4
    {
        public int aoc2025Day4()
        { 
            string inputPath = "C:\\CURRENT\\Testbed\\AdventOfCode\\AdventOfCode\\PuzzleInputs\\2025day4input.txt";



            ////Read from input file
            string[] inputs = System.IO.File.ReadAllLines(inputPath);
            //position is accessible if there is fewer than four rolls ('@') in any adjacent direction (8 possible directions)
            List<Present> accessibleRolls = new List<Present>();

            // check directions. {-1, -1}, {-1, 0}, {-1, 1}, {0, -1}, {0, 1}, {1, -1}, {1, 0}, {1, 1}. Count '@' in those positions 
            int[][] directions = new int[8][]
            {
                new int[]{-1, -1},
                new int[]{-1, 0},
                new int[]{-1, 1},
                new int[]{0, -1},
                new int[]{0, 1},
                new int[]{1, -1},
                new int[]{1, 0},
                new int[]{1, 1}
            };
            //part 2, run the loop until no more accessible rolls found. (roll replaced with '.' when marked as accessible)
            bool rollsRemaining = true;
            while (rollsRemaining)
            {
                int rollCounter = 0;
                //y direction (row)
                for (int y = 0; y < inputs.Length; y++)
                {
                    //x direction (column)
                    for (int x = 0; x < inputs[y].Length; x++)
                    {
                        //if present
                        if (inputs[x][y] == '@')
                        {
                            int adjacentRolls = 0;
                            //check adjacent positions
                            //if (x>0 && y>0 && inputs[x - 1][y - 1] == '@') adjacentRolls++; //bottom left roll found.
                            foreach (var direction in directions)
                            {
                                int newX = x + direction[0];
                                int newY = y + direction[1];
                                //check bounds
                                if (newX >= 0 && newX < inputs.Length && newY >= 0 && newY < inputs[newX].Length)
                                {
                                    if (inputs[newX][newY] == '@')
                                    {
                                        adjacentRolls++;
                                    }
                                }
                            }
                            //Part 1. fewer than 4 adjacent rolls
                            if (adjacentRolls < 4)
                            {
                                accessibleRolls.Add(new Present(x, y));
                                //Part 2. If accessible, remove it. Repalce '@' with '.'\
                                inputs[x] = inputs[x].Remove(y, 1).Insert(y, ".");  //get around string immutability
                                rollCounter++;

                            }
                        }
                    }

                }
                if (rollCounter == 0) rollsRemaining = false;

                Console.WriteLine("Accessible Rolls this scan: " + rollCounter);
            }
            Console.WriteLine("Total Accessible Rolls: " + accessibleRolls.Count);


            return accessibleRolls.Count;
        }

        private struct Present
        {
            public int X;
            public int Y;
            public Present(int x, int y)
            {
                X = x;
                Y = y;
            }
        }



    }
}
