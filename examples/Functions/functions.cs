public class FunctionExample {
    
    //Functions are named blocks of re-usable code.
    //If a function belongs to a class, like these do, we call it a 'Method'.
    
    //Functions are defined much like variables: a return type then a name. After the name, we use parentheses.
    //The parentheses are how C# know's it's a function. Inside them, we put parameters, variables used by the function.
    public string Emphasize(string parameter)
    {
        return parameter+"!";
    }
    
    //If we don't define a scope, they are private by default.
    //If we don't return a value, we have to use the "void" placeholder as the functions return type.
    void Banana()
    {
        //Calling a function is as simple as typing it's name - don't forget the parentheses and arguments (data to pass into the function) that matches the functions parameters (input variables).
        string ouput = Emphasize("Bananas");
        Console.WriteLine(ouput);
    }
    
    //Functions can have any number of arguments.
    public int SumFourThings(int a, int b,int c, int d)
    {
        return a+b;
    }

    public void Test()
    {
        //When calling a function, we pass the data in using the order of the parameters.
        //The names of parameters don't matter, but are important for readability.
        int sum = SumFourThings(1,2,3,4);

        //  

        //You can define functions inside of other functions.
        //This isn't very common.
        //It's a bit of a red flag: is there another way to structure your code? Maybe a static Utilities class.
        string SayHello()
        {
            return "Hello";
        }

        Console.WriteLine(SayHello());
    }

}