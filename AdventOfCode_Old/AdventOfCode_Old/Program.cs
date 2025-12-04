// See https://aka.ms/new-console-template for more information





using System.Text.RegularExpressions;
//////////////////
/// ///////
/// aoc 2015 day 6.
string[] inputs = System.IO.File.ReadAllLines(@"C:\CURRENT\Testbed\AdventOfCode\AdventOfCode_Old\AdventOfCode_Old\inputs.txt");

//create grid of lights and initialize all to 0.
//, multidimensional array
//part 1, lights can be 0 or 1.
//bool[,] lights = new bool[1000,1000];

//part 2, lights have brightness need to be int.
int[,] lights = new int[1000,1000];

const int TURNOFF = 0;
const int TURNON  = 1;
const int TOGGLE  = 2;


foreach(string instr in inputs)
{
    //quickest way I can think of to parse the coords. assumes no mistakes in input.
    var coordinates = Regex.Matches(instr, @"\d+,\d+");
    int[] startCoord = new int[]{int.Parse(coordinates[0].Value.Split(',')[0]), int.Parse(coordinates[0].Value.Split(',')[1])};
    int[] endCoord = new[]{int.Parse(coordinates[1].Value.Split(',')[0]), int.Parse(coordinates[1].Value.Split(',')[1])};
    //Console.WriteLine(String.Format(@"Instruction raw: {0} StartCoordinate = {1} End Coordinate: {2}", instr,startCoord[0]+" " +startCoord[1], endCoord[0] + " " + endCoord[1]));
    //set range  to true
    //feel like there's a neat way to turn on or off or toggle in one loop.
    int instrOperation = ParseOp(instr);
    //loop through given range performing the given operation.
    for(int x = startCoord[0]; x <= endCoord[0]; x++)
    {
        for(int y =startCoord[1]; y <= endCoord[1]; y++)
        {
                switch (instrOperation)
                {
                    case TURNOFF:
                    {
                        //part 2 lower brightness by 1, 0 min.
                        if (lights[x,y]> 0) lights[x,y]--;
                        break;
                    }
                    case TURNON: //brightness+1
                    {
                        lights[x,y]++;
                        break;
                        
                    }
                    case TOGGLE: //brightness+2
                    {
                        lights[x,y]+=2;
                        break;
                        
                    }
                }
        }
    }




}

int lightsON = 0;
int lightsTotalBrightness =0;
for(int x = 0; x < lights.GetLength(0); x++)
{
    for(int y = 0; y < lights.GetLength(1); y++)
    {
        if(lights[x,y]>0){
            // Console.ForegroundColor = (ConsoleColor)new Random().Next(1, 8); // Use 8 ANSI colors
            // Console.Write('■');
            // Console.ResetColor();
            lightsON++;
            lightsTotalBrightness+=lights[x,y];
        }
        else
        {            
            //Console.Write(' '); // Changed to space for off lights
        }
    }
    Console.WriteLine();
}
Console.WriteLine("Lights ON: " + lightsON + "Total Brightness: " + lightsTotalBrightness);

//parse operation
int ParseOp(string instr)
{
    if (instr.StartsWith("turn on")) return TURNON;
    if (instr.StartsWith("turn off")) return TURNOFF;
    return TOGGLE;
}


///////////





// ///////////
// /// 
// /// Advent of Code 2015 - Day 5
// using System.Text.RegularExpressions;

// //string input ="kjasfdlkajsfmxiusafjadfiejwp";
// string[] input = System.IO.File.ReadAllLines(@"C:\CURRENT\Testbed\AdventOfCode\AdventOfCode_Old\AdventOfCode_Old\inputs.txt");


// var vowels = new HashSet<char>{'a','e','i','o','u'};
// int niceStrings = 0;
// foreach(string line in input)
// {
//     //niceStrings = Part1NiceString(vowels, niceStrings, line);
//     niceStrings = Part2NiceString(niceStrings, line);
// }

// Console.WriteLine("Total Nice Strings: " + niceStrings);

// static int Part2NiceString(int niceStrings, string line)
// {
//     bool niceString = false;
//     bool hasRepeatSequence = false;
//     bool hasRepeatSandwich = false;
//     Match repeatSequence = null;
//     Match repeatSandiwch = null;

//     //part 2:
//     //any two letters that repeat at least twice.
//     //contains a letter that repeats with one letter sandwiched between them.
//     //answer incorrect. original repeat regex:
// //"(.{2}).*(\1)"

//     //match repeat sequence of two letters, no overlap.
//     if ((repeatSequence = Regex.Match(line, @"(.{2}).*(\1)")).Success)
//     {
//         hasRepeatSequence = true;
//     }
//     //match letter sandwich. 
//     if ((repeatSandiwch = Regex.Match(line, @"(.).(\1)")).Success)
//     {
//         hasRepeatSandwich = true;
//     }
//     if (hasRepeatSandwich && hasRepeatSequence)
//     {
//         //Console.WriteLine("has enough vowels and a double letter and no illegals");
//         niceStrings++;
//         niceString = true;
//     }
//     Console.WriteLine(String.Format("Line: {0} Nice:{4} HasRepeatSequence: {1} {5} HasRepeatSandwich: {2} {6}", line, hasRepeatSequence, hasRepeatSandwich, repeatSequence.Value, niceString, repeatSequence.Value, repeatSandiwch.Value));
    
//     return niceStrings;
// }






// static int Part1NiceString(HashSet<char> vowels, int niceStrings, string line)
// {
//     int vowelCount = 0;
//     bool hasDoubleLetter = false;
//     bool containsIllegal = false;
//     bool niceString = false;
//     Match illegalMatch = null;
//     Match doubleLetterMatch = null;
//     //loop through chars to count vowels.
//     foreach (char c in line)
//     {
//         if (vowels.Contains(c))
//         {
//             vowelCount++;
//         }
//     }

//     if ((doubleLetterMatch = Regex.Match(line, @"(.)\1")).Success)
//     {
//         hasDoubleLetter = true;
//     }

//     if ((illegalMatch = Regex.Match(line, @"ab|cd|pq|xy")).Success)
//     {
//         containsIllegal = true;
//     }
//     if (vowelCount >= 3 && hasDoubleLetter && !containsIllegal)
//     {
//         //Console.WriteLine("has enough vowels and a double letter and no illegals");
//         niceStrings++;
//         niceString = true;
//     }
//     Console.WriteLine(String.Format("Line: {0} Nice:{5} VowelCount: {1} HasDoubleLetter: {2} {6} ContainsIllegal: {3} {4}", line, vowelCount, hasDoubleLetter, containsIllegal, illegalMatch.Value, niceString, doubleLetterMatch.Value));
//     return niceStrings;
// }
///////////
/// 
/// 
/// 
////////////
// //adventof code 2015 day 4  

// string input = "iwrupvqb";

// for (int i = 0; i < int.MaxValue; i++)
// {
//     var hashHex = CreateMD5(input+i.ToString());
//     if (hashHex.Substring(0,6) == "000000")
//     {
//         Console.WriteLine("number addedto end of input: " +i.ToString() + "   HashHex " + hashHex );
//         break;
//     }


// }

// //no idea this was a thing.
// //stackoverflow  https://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string
// string CreateMD5(string input)
// {
//     // Use input string to calculate MD5 hash
//     using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
//     {
//         byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
//         byte[] hashBytes = md5.ComputeHash(inputBytes);

//         return Convert.ToHexString(hashBytes); 
//     }
// }

////////////////


// string[] lines = System.IO.File.ReadAllLines(@"C:\CURRENT\Testbed\AdventOfCode\AdventOfCode_Old\AdventOfCode_Old\inputs.txt");

// //starting coordinates

//AoC 2015 Day 3.  
//I initalially wanted to do a dictionary to get move values, and then wouldn't need the switch.
// int[] grid = new int[2]{0,0}; //assumes starting at 0,0.
// List<House> visitedHouses = new List<House>{new House{xCoord= 0, yCoord =0}}; //starting house already added.
// //part 2, robot goes after santa.
// int[] gridRobot = new int[2]{0,0}; //robot starting at 0,0.
// bool santaTurn = true;
// foreach (string line in lines)
// {
//     foreach (char c in line)
//     {
//         switch(c)
//         {
//             //north
//             case '^':
//             //i don't love having the turn flag on every case, but it works.
//                 if (santaTurn)
//                 {
//                     grid[1]++;
//                 }
//                 else
//                 {
//                     gridRobot[1]++;
//                 }
//                 break;
//             //south
//             case 'v':
//             if (santaTurn)
//                 {
//                     grid[1]--;
//                 }
//                 else
//                 {
//                     gridRobot[1]--;
//                 }

//                 break; 
//             //east           
//             case '>':
//             if (santaTurn)
//                 {
//                     grid[0]++;
//                 }
//                 else
//                 {
//                     gridRobot[0]++;
//                 }

//                 break;
//                 break;  
//             //west          
//             case '<':
//             if (santaTurn)
//                 {
//                     grid[0]--;
//                 }
//                 else
//                 {
//                     gridRobot[0]--;
//                 }

//                  break;
//                 break;
//             default:
//                 break;
//         }
//         if (santaTurn)
//         {
//             House currentHouse = new House{xCoord= grid[0], yCoord = grid[1]};
//             visitedHouses.Add(currentHouse);
//             santaTurn = false;
//         }
//         else
//         {
//             House currentHouse = new House{xCoord= gridRobot[0], yCoord = gridRobot[1]};
//             visitedHouses.Add(currentHouse);
//             santaTurn = true;
//         }
//         //House currentHouse = new House{xCoord= grid[0], yCoord = grid[1]};
//         //add location to visitedhouses
//         //part 1 fine to count mulitple visits to same house.
//         //visitedHouses.Add(currentHouse);

//         //check to see if house alread visited
//         // if (visitedHouses.Contains(currentHouse)    )
//         // {
//         //     visitedHouses.Add(currentHouse);
//         // }
//     }
// }
// Console.WriteLine("Total Distinct Houses " + visitedHouses.Distinct().Count()  );


// //coordinates of a house.
// public struct House{
//     public int xCoord;
//     public int yCoord;
// };



////////////////

// //Advent of Code 2015 - Day 1   
// int start = 0;
// int currentFloor = start;
// int basementIndex = 0;
// string[] lines = System.IO.File.ReadAllLines(@"C:\CURRENT\Testbed\AdventOfCode\AdventOfCode_Old\AdventOfCode_Old\inputs.txt");
// foreach (string line in lines)
// {
//     for(int i = 0; i < line.Length; i++)
//     {
//         char c = line[i];

//         if (c=='(')
//         { 
//             currentFloor ++;
//         }
//         else if (c==')')
//         { 
//             currentFloor --;
//         }
//         if (currentFloor < 0 && basementIndex == 0)
//         {
//             basementIndex = i+1;

//         }
//     }
// }
// Console.WriteLine("Final Floor: " + currentFloor + " Basement entered at position: " + basementIndex    );


////////////////

// //Advent of Code 2015 - Day 2
// int wrappingPaperTotal = 0;
// int ribbonTOtal = 0;
// string[] lines = System.IO.File.ReadAllLines(@"C:\CURRENT\Testbed\AdventOfCode\AdventOfCode_Old\AdventOfCode_Old\inputs.txt");
// foreach (string line in lines)
// {
//     string[] dimensions = line.Split('x');
//     int l = int.Parse(dimensions[0]);
//     int w = int.Parse(dimensions[1]);
//     int h = int.Parse(dimensions[2]);
//     //get surface area of each side.
//     int side1sa = l * w;
//     int side2sa = w * h;
//     int side3sa = h * l;

//     //perimeter of each side
//     int side1p = 2*(l + w);
//     int side2p = 2*(w + h);
//     int side3p = 2*(h + l);

//     //smallest side sa
//     int smallestSidesa = Math.Min(side1sa, Math.Min(side2sa, side3sa));
//     //smallest side per (ribbon length)

//     int smallestSidep = Math.Min(side1p, Math.Min(side2p, side3p));
//     //cubic volume (bow length)
//     int cubicVolume = l * w * h;


//     int surfaceArea = 2 * side1sa + 2 * side2sa + 2 * side3sa;
//     int presentPaperNeeded = surfaceArea + smallestSidesa;

//     wrappingPaperTotal += presentPaperNeeded;
//     ribbonTOtal+= smallestSidep + cubicVolume;
//     Console.WriteLine(String.Format(line + " --- Paper Needed: {0} Ribbon needed:{1}", presentPaperNeeded, smallestSidep + cubicVolume));

// }
// Console.WriteLine("Total Wrapping Paper Needed: " + wrappingPaperTotal + " Total Ribbon Needed: " + ribbonTOtal );

////////////////