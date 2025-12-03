using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class _2025Day2
    {

        public Int64 aoc2025Day2()
        {
            //puzzle input is single line, comma separated ranges.
            //each range is an starting rStart separated by a '-' followed by another string rEnd

            int invalidIDCount = 0;

            string inputPath = "C:\\CURRENT\\Testbed\\AdventOfCode\\AdventOfCode\\2025day2input.txt";

            string[] inputs = System.IO.File.ReadAllLines("C:\\CURRENT\\Testbed\\AdventOfCode\\AdventOfCode\\2025day2input.txt")[0].Split(',');

            Int64 invalidCount = 0;
            Int64 invalidAmount = 0;
            //InvalidProcessingPart1(ref invalidIDCount, inputs, ref invalidAmount);
            InvalidProcessingPart2(ref invalidIDCount, inputs, ref invalidAmount);


            return invalidAmount;

        }

        private static void InvalidProcessingPart1(ref int invalidIDCount, string[] inputs, ref long invalidAmount)
        {
            foreach (string range in inputs)
            {
                string[] bounds = range.Split('-');
                Int64 rStart = Int64.Parse(bounds[0]);
                Int64 rEnd = Int64.Parse(bounds[1]);

                //Console.WriteLine(String.Format("{0}-{1}", rStart, rEnd));

                List<Int64> invalidIds = new List<Int64>();
                //Process each range here
                for (Int64 i = rStart; i <= rEnd; i++)
                {
                    //i is considered invalid if it contains a sequence of digits repeated twice.
                    if (Regex.IsMatch(i.ToString(), @"^(.+)\1$"))
                    {
                        invalidIds.Add(i);
                        invalidIDCount++;
                        invalidAmount += i;
                    }
                }

                if (invalidIds.Count > 0)
                {

                    Console.WriteLine(String.Format("{0}-{1}\t\t\t\t\t\tInvalid IDs found: {2}", rStart, rEnd, string.Join(", ", invalidIds)));

                    invalidIDCount += invalidIds.Count;
                }
                else
                {
                    Console.WriteLine(String.Format("{0}-{1}", rStart, rEnd));
                }
            }

            Console.WriteLine(String.Format("Total Invalid IDs found: {0}\t Sum of invalidIds: {1}", invalidIDCount, invalidAmount));
        }

        private static void InvalidProcessingPart2(ref int invalidIDCount, string[] inputs, ref long invalidAmount)
        {
            foreach (string range in inputs)
            {
                string[] bounds = range.Split('-');
                Int64 rStart = Int64.Parse(bounds[0]);
                Int64 rEnd = Int64.Parse(bounds[1]);

                //Console.WriteLine(String.Format("{0}-{1}", rStart, rEnd));

                List<Int64> invalidIds = new List<Int64>();
                //Process each range here
                for (Int64 i = rStart; i <= rEnd; i++)
                {
                    //For part 2. i is invalid if it contains a sequence repeated >= twice
                    if (Regex.IsMatch(i.ToString(), @"^(.+)\1{1,}$"))
                    {
                        invalidIds.Add(i);
                        invalidIDCount++;
                        invalidAmount += i;
                    }
                }

                if (invalidIds.Count > 0)
                {

                    //Console.WriteLine(String.Format("{0}-{1}\t\tInvalid IDs found: {2}", rStart, rEnd, string.Join(", ", invalidIds)));

                    invalidIDCount += invalidIds.Count;
                }
                else
                {
                    //Console.WriteLine(String.Format("{0}-{1}", rStart, rEnd));
                }
            }

            Console.WriteLine(String.Format("Total Invalid IDs found: {0}\t Sum of invalidIds: {1}", invalidIDCount, invalidAmount));
        }




    }
}
