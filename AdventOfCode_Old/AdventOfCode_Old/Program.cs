// See https://aka.ms/new-console-template for more information

/*
//Advent of Code 2015 - Day 1   
int start = 0;
int currentFloor = start;
int basementIndex = 0;
string[] lines = System.IO.File.ReadAllLines(@"C:\CURRENT\Testbed\AdventOfCode\AdventOfCode_Old\AdventOfCode_Old\inputs.txt");
foreach (string line in lines)
{
    for(int i = 0; i < line.Length; i++)
    {
        char c = line[i];
        
        if (c=='(')
        { 
            currentFloor ++;
        }
        else if (c==')')
        { 
            currentFloor --;
        }
        if (currentFloor < 0 && basementIndex == 0)
        {
            basementIndex = i+1;
            
        }
    }
}
Console.WriteLine("Final Floor: " + currentFloor + " Basement entered at position: " + basementIndex    );
*/


//Advent of Code 2015 - Day 2
int wrappingPaperTotal = 0;
int ribbonTOtal = 0;
string[] lines = System.IO.File.ReadAllLines(@"C:\CURRENT\Testbed\AdventOfCode\AdventOfCode_Old\AdventOfCode_Old\inputs.txt");
foreach (string line in lines)
{
    string[] dimensions = line.Split('x');
    int l = int.Parse(dimensions[0]);
    int w = int.Parse(dimensions[1]);
    int h = int.Parse(dimensions[2]);
    //get surface area of each side.
    int side1sa = l * w;
    int side2sa = w * h;
    int side3sa = h * l;

    //perimeter of each side
    int side1p = 2*(l + w);
    int side2p = 2*(w + h);
    int side3p = 2*(h + l);

    //smallest side sa
    int smallestSidesa = Math.Min(side1sa, Math.Min(side2sa, side3sa));
    //smallest side per (ribbon length)

    int smallestSidep = Math.Min(side1p, Math.Min(side2p, side3p));
    //cubic volume (bow length)
    int cubicVolume = l * w * h;


    int surfaceArea = 2 * side1sa + 2 * side2sa + 2 * side3sa;
    int presentPaperNeeded = surfaceArea + smallestSidesa;

    wrappingPaperTotal += presentPaperNeeded;
    ribbonTOtal+= smallestSidep + cubicVolume;
    Console.WriteLine(String.Format(line + " --- Paper Needed: {0} Ribbon needed:{1}", presentPaperNeeded, smallestSidep + cubicVolume));

}
Console.WriteLine("Total Wrapping Paper Needed: " + wrappingPaperTotal + " Total Ribbon Needed: " + ribbonTOtal );