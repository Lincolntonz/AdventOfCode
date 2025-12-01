// See https://aka.ms/new-console-template for more information


///Advent of Code 2025
int dial = 50;  //starting dial value
int countZeros = 0; //day1 count anytime dial lands on zero
int countWraps = 0; //day2 count anytime dial pass zero (wraps around below 0 or above 99)
int dialPossibilities = 100;  //100 possible integers on the dial
int maxDialValue = 99; //max dial value
string[] inputs = System.IO.File.ReadAllLines("C:\\CURRENT\\Testbed\\AdventOfCode\\AdventOfCode\\day1puzzleinput.txt");

foreach (string input in inputs)
{
    if (input != null && input.Length > 0)
    {
        
        bool wrapFlag = false;
        int dialstart = dial;
        char direction = input[0];
        int value = int.Parse(input[1..]);
        if (direction == 'R')
        {
            //
            //Wrap detection: if adding the value to the dial exceeds dialPossibilities (=100 would be landing on zero) and dial is not starting at 0, we wrapped   
            if (dial!=0  && (dial + value > dialPossibilities))
            {
                countWraps++;
                wrapFlag = true;
            }
            //turn right -- add and modulo incase > maxDial
            dial = (dial + value) % dialPossibilities;

        }
        else if (direction == 'L')
        {

            //wrap detection: if subtracting the value from the dial goes below 0 and dial is not starting at 0, we wrapped
            if (dial != 0 && dial - value < 0)
            {
                countWraps++;
                wrapFlag = true;
            }
            //int result = ((a - b) % m + m) % m;

            //turn left subtract. If negative, add to maxdial to wrap around. Final modulo is just incase
            dial = ((dial - value) % dialPossibilities + dialPossibilities) % dialPossibilities;
            
        }

        
        //day1 count anytime dial lands on zero.
        if (dial == 0)
        {

            countZeros++;
        }

        if (!wrapFlag)
        {
            Console.WriteLine(String.Format("Dial start:\t{0}\tDirection:\t{1}\tValue:\t{2}\tDial end:\t{3}", dialstart, direction, value, dial));
        }
        else
        {
            Console.WriteLine(String.Format("Dial start:\t{0}\tDirection:\t{1}\tValue:\t{2}\tDial end:\t{3}\tWRAPPED", dialstart, direction, value, dial));
        }
        
    }

}


//980 landed on zero,  2384 wrapped past zero.  Incorrect answer for day 2.
Console.WriteLine(String.Format("Landed on zero: {0}\tWrapped Past Zero: {1}",countZeros,(countZeros+countWraps).ToString()))  ;
