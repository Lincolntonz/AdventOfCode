// See https://aka.ms/new-console-template for more information


int dial = 50;
int countZeros = 0;
int maxDial = 100;  //100 ints on the dial    
string[] inputs = System.IO.File.ReadAllLines("C:\\CURRENT\\Testbed\\AdventOfCode\\AdventOfCode\\day1puzzleinput.txt");

foreach (string input in inputs)
{
    if (input != null && input.Length > 0)
    {
        int dialstart = dial;
        char direction = input[0];
        int value = int.Parse(input[1..]);
        if (direction == 'R')
        {
            //turn right -- add and modulo incase > maxDial
            dial = (dial + value) % maxDial;


        }
        else if (direction == 'L')
        {

            //int result = ((a - b) % m + m) % m;
            //turn left subtract. If negative, add to maxdial to wrap around. Final modulo is just incase
            dial = ((dial - value) % maxDial + maxDial) % maxDial;
        }
        Console.WriteLine(String.Format("Dial start:\t{0}\tDirection:\t{1}\tValue:\t{2}\tDial end:\t{3}", dialstart, direction, value, dial)); 
        if (dial == 0)
        {
            
            countZeros++;
        }   
    }

}



Console.WriteLine(countZeros.ToString()  );
