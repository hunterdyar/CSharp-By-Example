using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloWorldAnalyzer.ToParse
{
	//This is a person class.  
	//This comment has a few lines.  
	//This is a third line.  
	public class Person
	{
		public string Name { get; private set; }

		//This is a constructor. It looks like a function, but with no return type, and the name matches the class. Or, if you prefer, it looks like a function that has no name, and a return type of the class.  
		//  
		//'Constructors' are the code that is called when you create a new instance of a class: "p = new Person("Biff");" will call this function, and we simply set this Name variable to what is given ('biff')
		public Person(string name)
		{
			Name = name;
		}

		//This is a comment above the function 'speak'
		public string Speak()
		{
			//this is a comment return statement
			return string.Format("Hello! My name is{0}",
				Name);
		}
	}
}