public class FunctionExample {
    
    //Functions are named blocks of re-usable code.
    //If a function belongs to a class, like this one does, we call it a 'Methods'. 
    public void Test()
    {
        //you can define functions inside of other functions.  
        //This isn't very common. It's a bit of a red flag. Is there another way to structure your code? Maybe a static Utilities class.
        string SayHello()
        {
            return "Hello";
        }
        
        Console.WriteLine(SayHello());
    }
    
    //Functions are defined much like variables: a return type then a name. After the name, we use parenthesis.
    //The parenthesis are how C# know's it's a function. We can put 'parameters' into functions to 
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
    
    //
    public int Sum(int a, int b)
    {
        return a+b;
    }
}