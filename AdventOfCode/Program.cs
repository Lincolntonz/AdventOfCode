// See https://aka.ms/new-console-template for more information


///Advent of Code 2025
//int dial = 50;  //starting dial value
//int countZeros = 0; //day1 count anytime dial lands on zero
//int countWraps = 0; //day2 count anytime dial pass zero (wraps around below 0 or above 99)
//int dialPossibilities = 100;  //100 possible integers on the dial
//int maxDialValue = 99; //max dial value


/*
string Part2Original(string[] inputs)
{
    foreach (string input in inputs)
    {
        if (input != null && input.Length > 0)
        {

            // clear and easy-to-read step variables
            bool wrapFlag = false;
            int dialstart = dial;
            char direction = input[0];
            int value = int.Parse(input[1..]);

            // number of times we cross past 0 for this move (can be > 1)
            int wrapsThisMove = 0;

            if (direction == 'R')
            {
                // RIGHT turn: increasing the dial.
                // Number of times we pass 0 = floor((dial + value) / dialPossibilities)
                wrapsThisMove = (dial + value) / dialPossibilities;
                if (wrapsThisMove > 0)
                {
                    countWraps += wrapsThisMove;
                    wrapFlag = true;
                }

                // now apply the wrap using modulo
                dial = (dial + value) % dialPossibilities;
            }
            else if (direction == 'L')
            {
                // LEFT turn: decreasing the dial.
                // Otherwise number of times we pass 0 (moving left) is:
                // ceil((value - dial) / dialPossibilities) which in integer math becomes
                // (value - dial + dialPossibilities - 1) / dialPossibilities, but only when value > 0.

                if (value >= dial)
                {
                    wrapsThisMove = (value - dial + dialPossibilities - 1) / dialPossibilities;
                    if (wrapsThisMove > 0)
                    {
                        countWraps += wrapsThisMove;
                        wrapFlag = true;
                    }
                }

                // perform left turn with safe modulo that handles negatives
                dial = ((dial - value) % dialPossibilities + dialPossibilities) % dialPossibilities;
            }

            // count landing on zero separately if you still want that statistic
            if (dial == 0)
            {
                countZeros++;
            }

            // logging (indicate how many wraps happened this move)
            if (!wrapFlag)
            {
                Console.WriteLine(String.Format("Dial start:	{0}	Direction:	{1}	Value:	{2}	Dial end:	{3}\tTotal{4}", dialstart, direction, value, dial,countWraps));
            }
            else
            {
                Console.WriteLine(String.Format("Dial start:	{0}	Direction:	{1}	Value:	{2}	Dial end:	{3}\tTotalZeros{4} WRAPPED x{5}", dialstart, direction, value, dial,countWraps, wrapsThisMove));
            }

        }

    }
    return countWraps.ToString();
    ////980 landed on zero,  2384 wrapped past zero.  Incorrect answer for day 2.
    ////Console.WriteLine(String.Format("Landed on zero: {0}\tWrapped Past Zero: {1}",countZeros,(countZeros+countWraps).ToString()))  ;
    //    return countZeros.ToString();
    //}
}
*/





string[] inputs = System.IO.File.ReadAllLines("C:\\CURRENT\\Testbed\\AdventOfCode\\AdventOfCode\\day1puzzleinput.txt");

string[] inputs2 = {
"R50"
,"R50"
,"L50"
,"L50"
,"R75"
,"L50"
,"L25"
,"L75"
,"R50" };






///part 1 and 2 taken from a reddit solution. can't get my part 2 to work.
string Part1(string[] input)
{
    var d = 50;
    const int maxD = 100;
    var a = 0;
    foreach (var line in input)
    {
        // Determine direction of move. Right positive, left negative
        var v = line[0] == 'R' ? 1 : -1;
        //Parse move value
        if (!int.TryParse(line[1..], out var c)) throw new Exception("Invalid number detected!");
        //dial  is updated by value (subtracted or added based on direction)
        d += (v * c);
        //modulo max dial value to get the current dial position
        d %= maxD;
        //if dial position is a negative value, add max dial value to wrap around
        if (d < 0) d += maxD;
        //if dial is at zero, increment counter 
        if (d == 0) a++;
    }

    return "" + a;
}

 
string Part2(string[] input)
{
    var d = 50;
    const int maxD = 100;
    var a = 0; //zero points
    foreach (var line in input)
    {
        var v = line[0] == 'R' ? 1 : -1; //direction
        if (!int.TryParse(line[1..], out var c)) throw new Exception("Invalid number detected!");
        var distToZero = v == 1 ? maxD - d : d; //distance to zero based on direction
        if (distToZero > 0 && c >= distToZero) a++; //if distance value c, is greater than the distance to zero, we will pass zero at least once.
        a += (c - distToZero) / maxD; //add any additional full wraps past zero to zeros counter
        d += (v * c); //update dial position
        d %= maxD; //modulo max dial value to get the current dial position        
        if (d < 0) d += maxD;//if dial position is a negative value, add max dial value to wrap around

        Console.WriteLine(String.Format("Move: {0}  New Dial: {1}  Total Zeros: {2}", line, d, a));
    }

    return "" + a;
}


Console.WriteLine(Part2(inputs2));