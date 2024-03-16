public void Main() {
    //Here is a basic example if an if/else statement.
    //In C#, we need parenthesis after the if statement, and curly braces for the code.
    if(7%2 == 0){
        Console.WriteLine("7 is even")
    } else {
        Console.WriteLine("7 is odd")
    }
    //You can have an if statement without an else.
    if(8%4 == 0){
        Console.WriteLine("8 is divisible by 4")
    }
    
    //You can write "one-liners" without curly braces. This is nice for simple checks.  
    //The whitespace doesn't matter. The if statement "if()statement" simply does-or-doesn't execute the following statement. We usually enclose that statement in a statement block.
    //For those learning C#, I advise against writing code like this. CSharp doesn't care about line breaks or indentation, but this code looks like it does care about indentation, it's often misleading for students. 
    if(21%3 == 0)
       Console.WriteLine("21 is divisible by 3");
   
   //Consider the following situation. Both lines look like they are part of the if statement, but they aren't. Without curly braces, the if statement conditionally executes only a single following statement.
    // 
    //Because of this common mistake, I advise beginners to always use curly braces.
    if(false)
        Console.WriteLine("This line is part of the if statement.");
        Console.WriteLine("THis line is not. It will be executed no matter what is in the conditional.");

    //C# has conditional operators like && for "[and](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#conditional-logical-and-operator-)" and || for "[or](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#conditional-logical-or-operator-)".
    if(8%2 == 0 || 7%2 == 0) {
        Console.WriteLine("either 8 or 7 are even")
    }
    
    //You can chain if/else statements. It's not considered good practice to have long if/ifelse/ifelse/ifelse/ifelse/else chains. Sometimes you gotta do what you gotta do - I won't judge. 
    int num = 42;
    if(num<0) {
        Console.WriteLine(num, "is negative")
    } else if num < 10 {
        Console.WriteLine(num, "has 1 digit")
    } else {
        Console.WriteLine(num, "has multiple digits")
    }
}