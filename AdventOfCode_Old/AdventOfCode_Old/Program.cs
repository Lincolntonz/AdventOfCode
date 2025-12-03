// See https://aka.ms/new-console-template for more information




////////////
//adventof code 2015 day 4  

string input = "iwrupvqb";

for (int i = 0; i < int.MaxValue; i++)
{
    var hashHex = CreateMD5(input+i.ToString());
    if (hashHex.Substring(0,6) == "000000")
    {
        Console.WriteLine("number addedto end of input: " +i.ToString() + "   HashHex " + hashHex );
        break;
    }


}

//no idea this was a thing.
//stackoverflow  https://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string
string CreateMD5(string input)
{
    // Use input string to calculate MD5 hash
    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
    {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes); 
    }
}

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
