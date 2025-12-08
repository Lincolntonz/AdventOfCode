using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class _2025Day6
    {
        public string aoc2025Day6()
        {
            string inputPath = "C:\\CURRENT\\Testbed\\AdventOfCode\\AdventOfCode\\PuzzleInputs\\2025day6input.txt";
            string[] inputLines = System.IO.File.ReadAllLines(inputPath);

            List<string[]> splitLines = new List<string[]>();
            string[] arguments;
            string[] operators;
            //create grid of argumentrs
            //this banks on each line having an equal number of elements
            foreach (string line in inputLines)
            {
                // if (line != inputLines[inputLines.Length-1])
                // {
                //I wasn't accounting for possibly more than one space between columns (operators multi space). this ignores empty space.
                arguments = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                splitLines.Add(arguments);
                //}
                //else
                //{
                //     //operator is on last line.
                //     //I wasn't accounting for possibly more than one space between columns (operators multi space). this ignores empty space.
                //     operators = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                //     splitLines.Add(operators);
                // }

            }

            //long grandTotal = Part1(splitLines);
            long grandTotal = Part2(splitLines);

            var result = grandTotal.ToString();

            return result;
        }
        private static long Part2(List<string[]> splitLines)
        {
            StringBuilder stringBuilder = new StringBuilder();

            long grandTotalPart1 = 0;
            long grandTotalPart2 = 0;
            for (int x = 0; x < splitLines[0].Length; x++)
            {
                //solve vertically
                //increment column count. Max count will be the length of one of the arrays of arguments or operators (assuming equal).
                long columnValue = 0;

                //assumes operator line will be the last line in splitLines
                string op = splitLines[splitLines.Count - 1][x];

                List<string> listofArgumentsForColumn = new List<string>();

                for (int i = 0; i < splitLines.Count - 1; i++)
                {

                    //stringBuilder.Append(splitLines[x][i] + " ");

                    var argument = long.Parse(splitLines[i][x]);
                    listofArgumentsForColumn.Add(argument.ToString());
                    ///part 2
                    ///each digit of the argumentOverall is the argumentDigit
                    ///from right to left. IE: arguments  64,23,314,+   will be 4 + 431 + 623

                    //build array of arguments.

                    //pad zeroes or just wing it? zeros wont work in the multiplcation so just use Length and index

                    //get longest argument length.



                    switch (op)
                    {
                        case "+":
                            columnValue += argument;
                            stringBuilder.Append(" +" + argument);
                            break;
                        case "*":

                            columnValue = columnValue == 0 ? argument : argument * columnValue;  //column value initialized at 0, won't work for * operator
                            stringBuilder.Append(" *" + argument);
                            break;
                    }
                }

                 grandTotalPart2 += RightToLeftColumnOperation(listofArgumentsForColumn,op);

                grandTotalPart1 += columnValue;
                //stringBuilder.Append(" = " + columnValue.ToString());
                //stringBuilder.Append("\n");
            }



            //Console.WriteLine(stringBuilder.ToString());
            //Console.Write("Grand Total: " + grandTotalPart1);
            return grandTotalPart1;
        }

        private static long RightToLeftColumnOperation(List<string> listofArgumentsForColumn, string op)
        {
            StringBuilder sb = new StringBuilder();
            long total = op == "*" ? 1 : 0;

            var maxLength = 1;
            //get the maximum length of an element in arguments.
            foreach (string argument in listofArgumentsForColumn)
            {
                Console.WriteLine(argument);
                if (argument.Length > maxLength) maxLength = argument.Length;
            }
            //right to left, number of column times.
            StringBuilder columnValue = new StringBuilder();
            for (int col = maxLength - 1; col >= 0; col--)
            { 
                //for each argument.
                for (int i = 0; i < listofArgumentsForColumn.Count; i++)
                {
                    int argLength = listofArgumentsForColumn[i].Length;
                    int digitArgument;
                    //this particular argument will have listofArgumentsForColumn[i].Length # of digits. If this is less than the index col is at, skip. otherwise get it.
                   if (argLength > col && int.TryParse(listofArgumentsForColumn[i][col].ToString(), out digitArgument))
                    {
                        //Console.WriteLine(" digit!" + digitArgument.ToString());
                        columnValue.Append(digitArgument);
                        //switch (op)
                        //{
                        //    case "+":
                        //        //sb.Append(" +" + digitArgument);
                        //        break;
                        //    case "*":

                        //        columnValue = columnValue + digitArgument;  //column value initialized at 0, won't work for * operator
                        //        //sb.Append(" *" + digitArgument);
                        //        break;
                        //}
                    }


                }

                sb.Append(columnValue + " \n");
                Console.Write(columnValue.ToString() + "\n");
            }





                return total;
        }

        private static long Part1(List<string[]> splitLines)
        {
            StringBuilder stringBuilder = new StringBuilder();

            long grandTotalPart1 = 0;
            for (int x = 0; x < splitLines[0].Length; x++)
            {
                //solve vertically
                //increment column count. Max count will be the length of one of the arrays of arguments or operators (assuming equal).
                long columnValue = 0;

                //assumes operator line will be the last line in splitLines
                string op = splitLines[splitLines.Count - 1][x];

                for (int i = 0; i < splitLines.Count - 1; i++)
                {

                    //stringBuilder.Append(splitLines[x][i] + " ");

                    var argument = long.Parse(splitLines[i][x]);

                    switch (op)
                    {
                        case "+":
                            columnValue += argument;
                            stringBuilder.Append(" +" + argument);
                            break;
                        case "*":

                            columnValue = columnValue == 0 ? argument : argument * columnValue;  //column value initialized at 0, won't work for * operator
                            stringBuilder.Append(" *" + argument);
                            break;
                    }
                }
                grandTotalPart1 += columnValue;
                stringBuilder.Append(" = " + columnValue.ToString());
                stringBuilder.Append("\n");
            }



            Console.WriteLine(stringBuilder.ToString());
            Console.Write("Grand Total: " + grandTotalPart1);
            return grandTotalPart1;
        }
    }
}
