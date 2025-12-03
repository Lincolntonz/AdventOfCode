using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class _2025Day3
    {
        public string aoc2025Day3()
        {
            Int64 totalJoltage = 0;

            string inputPath = "C:\\CURRENT\\Testbed\\AdventOfCode\\AdventOfCode\\PuzzleInputs\\2025day3input.txt";

            string inputTest =
                                @"987654321111111
                                811111111111119
                                234234234234278
                                818181911112111";

            ////Read from input file
            string[] inputs = System.IO.File.ReadAllLines(inputPath);

            //Test input from string
            //string[] inputs = inputTest.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
            //foreach (string line in inputs)
            //{
            //    Console.WriteLine(line);
            //}


            joltageCalc(ref totalJoltage, inputs, 2);
            Console.WriteLine(String.Format("Total Joltage: {0}", totalJoltage));


            return totalJoltage.ToString();
        }


        private static void joltageCalc(ref Int64 totalJoltage ,string[] inputs, int batteryCount)
        {


            //process each line
            foreach (string line in inputs)
            {
                Int64 lineJoltage = 0;
                //Total joltage of the line is the highest value of any TWO(batteryCount) left to right digits within the line.
                int highestDigit = 0;
                int highestDigitIndex = -1;

                int[] selectedDigits = new int[batteryCount];
                int[] selectedDigitsIndex = new int[batteryCount];
                //for part 2 expand this to recursively find this for any batteryCount number of digits.
                //starting left to right. first loop will find the highest digit and place it in selectedDigits[batteryIndex]
                for (int batteryIndex = 0; batteryIndex < batteryCount; batteryIndex++)
                {
                    highestDigit = 0;
                    //find highest first digit in the line (excluding the last (batteryCount-1) digits since the final digits can't have enough batteries to their right.
                    for (int i = highestDigitIndex + 1; i < line.Length - (batteryCount- batteryIndex-1); i++)
                    {
                        int digit = int.Parse(line[i].ToString());
                        if (digit > highestDigit)
                        {
                            highestDigit = digit;
                            highestDigitIndex = i;
                            selectedDigits[batteryIndex] = digit;
                            selectedDigitsIndex[batteryIndex] = i;
                           // i++; //move index forward to start searching for next highest digit to the right.

                        }   
                    }

                    //part 1, battercount=2 so i just manually looped for the second digit.
                    ////Now find the next highest digit to the right of the first highest digit.
                    //int secondHighestDigit = 0;
                    //int secondHighestDigitIndex = -1;
                    //for (int j = highestDigitIndex + 1; j < line.Length; j++)
                    //{
                    //    int digit = int.Parse(line[j].ToString());
                    //    if (digit > secondHighestDigit)
                    //    {
                    //        secondHighestDigit = digit;
                    //        secondHighestDigitIndex = j;
                    //    }
                    //}



                }
                string selectedDigitsStr = string.Join("", selectedDigits);
                lineJoltage = Int64.Parse(selectedDigitsStr);
                totalJoltage += lineJoltage;
                Console.WriteLine(String.Format("Line: {0}\t  Line Joltage: {1}", line, lineJoltage));
            }




        }



    }
}
