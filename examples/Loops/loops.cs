bool conditional = true;

//A while loop has the same structure as an if statement, but it will repeatedly run the code in block until the condition stops being true.
//Also like an if statement, if it's false (as this one is), it will skip it and never run.
while (conditional)
{
	Console.Write("Run Forever");
	//The break keyword will end the loop, regardless of the conditional.
	//Without it, this while(true) loop would be an infinite loop, and the program would crash.
	break;
}

//The following loop will run 10 times. The counter starts at 0 and goes up till 9.
//On the last run, it's increased to 10. 10 is not less than 10, so it stops running and the code moves on.
//That's 10 total runs. The output of this code would be "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, "  

//This runs once before the loop
int counter = 0;
//The conditional is checked every time, and returns true or false.
while (counter < 10)
{
	Console.Write(counter+", ");
	//This runs every single time, after the rest of the code above.
	counter++;
}

//---

//The syntax of a counter is convenient, so there's a different type of loop that does all these parts too. It's called a for loop.
//The for loop has the same 3 parts that were written in the previous while loop: a part that runs once at the beginning, a conditional that is checked every time, and a part that runs at the end of each loop.
//Defined in that order.
for (int i = 0; i < 10; i++)
{
	Console.Write(i+", ");
}

//Using a for loop instead of a while loop keeps it so that all of the 'loop control' logic is written in one place, while the 'things we do in a loop' is written elsewhere.
for (int j = 10; j>=0;j--)
{
	Console.WriteLine(j);
}
Console.WriteLine("Blast Off!");
//
//
//It's a very common pattern to use a for loop to run through all the items in an array once.
string[] someArray = new string[5];
//
for (int i = 0; i < someArray.Length; i++)
{
	string item = someArray[i];
	if (item == "")
	{
		//The 'continue' keyword, inside of a loop, will skip ahead to the next iteration.
		continue;
	}else if (item == "should-break")
	{
		//the 'break' keyword will stop the loop executing entirely.
		break;
	}
}