//The + operator will concatenate strings.
string name = "Hunter";
Console.Write("Hello, " + name);

//The String.Format method is a much more convenient way to write these kinds of expressions.
float amount = 123.4f; 
string output = String.Format("There are {0} grams of bananas.",amount);

//String.Format supports multiple items. Just increase the number, providing the items as arguments.
string multipleThings = String.Format("On {0:d}, {1} has {2} grams of bananas.",DateTime.Now, name, amount);

//You can control the formatting of the string using different [format specifiers](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings).
DateTime time = DateTime.Now;
string timeOutput = String.Format("The date is {0:d} and the time is {0:t}",time);

//A much more convenient syntax for formatting strings is called the 'Interpolated String'
//Use a dollar sign, then put string values (anything with a ToString() function)  in curly braxes.
//This is my preferred approach for formatting strings, since it is very easy to read.
string interpOutput = $"My name is {name}, and today is {time.DayOfWeek}.";
