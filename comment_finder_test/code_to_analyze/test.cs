using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloWorldAnalyzer.ToParse
{
	//This is a person class
	//This comment has a few lines.
	public class Person
	{
		public string Name { get; private set; }

		//This is a constructor
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