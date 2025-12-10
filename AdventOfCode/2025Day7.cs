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

            int[] startingIndex = new int[] { 0, input[0].IndexOf('S')};  ///starting point will be 'S' on first line.
            Console.WriteLine("starting index: " + startingIndex);
            //test input read

            List<int> beamsOnPreviousRow = new List<int>(); //index value of each beam on previousRow
            beamsOnPreviousRow.Add(startingIndex[1]);
            List<int> beamsOnthisRow = new List<int>(); //index of each beam on previousRow
            List<int> beamsOnNextRow = new List<int>();
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
                            //beam to the right index cannot be greater than the max length of the line.
                            beamsOnNextRow.Add(incomingBeamColumn + 1 > tachyonGrid[0].Length - 1 ? tachyonGrid[0].Length - 1 : incomingBeamColumn + 1);
                            beamSplit++;
                            break;
                        case '.':
                            //empty space. beam will be here. 
                            //string immutable, removeat and ad to get around this
                            tachyonGrid[i] = ReplaceAt(tachyonGrid[i], incomingBeamColumn, '|');
                            beamsOnNextRow.Add(incomingBeamColumn);
                            break ;
                    }

                }

                beamsOnPreviousRow.Clear();
                beamsOnPreviousRow.AddRange(beamsOnNextRow);
                beamsOnNextRow.Clear();

                Console.WriteLine(tachyonGrid[i]);
                
            }
            Console.Write("Total number of beam splits: " + beamSplit);
            return beamSplit.ToString();
        }



        public static string ReplaceAt(string input, int index, char newChar)
        {
            if (input == null) { throw new ArgumentNullException("input"); }
            char[] chars = input.ToCharArray();// string to char array
            chars[index] = newChar; //replace index with newCHar
            return new string(chars); //put back to string and return.
        }
    }
}
