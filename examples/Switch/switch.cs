public class SwitchExample {
	public enum Sport
	{
		Football,
		Baseball,
		IceHockey,
		Basketball
	}
	public void Main()
	{

		int i = 2;
		//Switch statements can work like an if/else statement, but instead of checking every condition in order, the code execution will jump to a single branch.  
		//This can be a much more efficient way to check a lot of conditions, when only one case could be true.
		switch (i)
		{
			case 0:
				Console.Write("Zero");
				break;
			case 1: //Curly braces can be included for readability in longer switch statements.
			{
				Console.Write("One");
				break;
			}
			case 2:
				Console.Write("Two");
				break;
			//if you don't include the 'break', then the code will "fall through", until you do hit a break.  
			//This can be convenient for handling more complex cases.
			case 3:
			case 4:
			case 5:
				Console.Write("Three-through-Five");
				break;
			case 6:
				Console.Write("Six");
				break;
			default:
				Console.Write("Some Number");
				break;
		}

		//It's useful to use a switch statement when working with enums.
		Sport sport = Sport.Football;
		string objectShape = "";
		switch (sport)
		{
			case Sport.IceHockey:
				objectShape = "Short Cylinder";
				break;
			case Sport.Football:
				objectShape = "Prolate Spheroid";
				break;
			case Sport.Baseball:
			case Sport.Basketball:
				objectShape = "Sphere";
				break;
			default:
				objectShape = "Unknown";
				break;
		}
		Console.WriteLine($"{sport.ToString()} uses a {objectShape} shape item.");
	}
}