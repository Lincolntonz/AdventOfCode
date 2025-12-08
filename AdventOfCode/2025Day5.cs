using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode
{
    internal class _2025Day5
    {

        public string aoc2025Day5()
        {
            string inputPath = "C:\\CURRENT\\Testbed\\AdventOfCode\\AdventOfCode\\PuzzleInputs\\2025day5input.txt";
            string[] inputLines = System.IO.File.ReadAllLines(inputPath);

            List<Int64[]> freshRanges = new List<Int64[]>();

            List<Int64> ingredientIds = new List<Int64>();
            List<Int64> freshIngredientIds = new List<Int64>();

            foreach (string line in inputLines)
            {
                //line is a specifying a range of fresh ingredient ids.
                if (line.Contains('-'))
                {
                    string[] bounds = line.Split('-');
                    Int64 rStart = Int64.Parse(bounds[0]);
                    Int64 rEnd = Int64.Parse(bounds[1]);
                    freshRanges.Add(new Int64[2] { rStart, rEnd });
                }
                else//else line is an available ingredientid (or an empty seperator)
                {
                    if (line.Trim() != "")
                    {
                        ingredientIds.Add(Int64.Parse(line.Trim()));
                    }
                }
            }

            Console.WriteLine("Fresh ingredient ids\n");

            //part 1
            //long freshIds = Part1(ranges, ingredientIds, freshIngredientIds);

            //part 2.
            //would be easy but ranges will overlap. need to make a list of ranges that don't overlap
            long freshIds = 0;

            List<long[]> mergedRanges = MergeRangesNonOverlapping(freshRanges);

            //count total ingredientids in the merged no-overlap range.
            foreach (long[] range in mergedRanges)
            {
                Console.WriteLine(String.Format(@"[{0},{1}]", range[0], range[1]));

                freshIds += (range[1] - range[0]) + 1;
                //for (long i = range[0]; i <= range[1]; i++)
                //{
                //    Console.WriteLine(i);
                //}

            }



            Console.WriteLine("Total fresh ingredients: " + freshIds);

            return freshIds.ToString();

        }
        /// <summary>
        /// Given a list of arrays long[] range where range[0] = Start and range[1] = End.  This will merge the ranges so that no two ranges overlap each other.
        /// </summary>
        /// <param name="ranges"></param>
        /// <returns></returns>
        private static List<long[]> MergeRangesNonOverlapping(List<long[]> ranges)
        {
            //Sort the list of ranges from min->max based on the start [0] element of each pair.
            var minSort = ranges.OrderBy(arr => arr[0]).ToList();
            //new list containing merged ranges.
            List<long[]> mergedRanges = new List<long[]>();

            for (int i = 0; i < minSort.Count; i++)
            {
                //add first range to mergedlist.
                if (i == 0)
                {
                    mergedRanges.Add(minSort[i]);
                }
                else
                {
                    var previousStart = mergedRanges[mergedRanges.Count - 1][0];
                    var previousEnd = mergedRanges[mergedRanges.Count - 1][1];
                    var currentStart = minSort[i][0];
                    var currentEnd = minSort[i][1];
                    //compare first start of this range vs the start of the last element added to the merged list.
                    if (currentStart <= previousEnd)
                    {
                        //overlap -- set end of the last merged range to be the largest of the end values.
                        mergedRanges[mergedRanges.Count - 1][1] = long.Max(mergedRanges[mergedRanges.Count - 1][1], minSort[i][1]);
                    }
                    else
                    {
                        //no overlap. add to merged range.
                        mergedRanges.Add(new long[] { currentStart, currentEnd });
                    }
                }
            }

            return mergedRanges;
        }

        private static long Part1(List<long[]> freshRanges, List<long> ingredientIds, List<long> freshIngredientIds)
        {

            ///Part 1 ---  -- loop through igredients. then check if within each range using   rstart <= ingredient <= rEnd
            foreach (Int64 id in ingredientIds)
            {
                //check to see if id already fresh
                if (!freshIngredientIds.Contains(id))
                {
                    //check for freshness
                    foreach (Int64[] range in freshRanges)
                    {
                        if ((range[0] <= id) && (id <= range[1]))
                        {
                            //check again to see if we've added it on a previous range.
                            if (!freshIngredientIds.Contains(id))
                            {
                                freshIngredientIds.Add(id);
                                Console.WriteLine(id.ToString());
                                continue;
                            }
                        }
                    }
                }
            }



            Int64 freshIds = freshIngredientIds.Count();
            return freshIds;
            //  **BAD** works but takes too long -this is the wrong way. takes forever to loop through each range. 
            //for each range, check to see if ingredient is within the range. If it is, add it to the freshIngredientIds list. only add it once.


            //foreach (var range in ranges) 
            //{
            //    for (Int64 id = range[0]; id <= range[1]; id++)
            //    {
            //        if (ingredientIds.Contains(id))
            //        {
            //            if (!freshIngredientIds.Contains(id))
            //            {
            //                freshIngredientIds.Add(id);
            //                Console.WriteLine(id.ToString());
            //            }
            //        }
            //    }
            //}

        }


    }
}
