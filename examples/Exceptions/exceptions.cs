using System;

class ExceptionExamples
{
    //Errors can be created with the **throw** keyword. C# comes with a number of included Error types, which are helpful for error readability.
    //You shouldn't return the error object, but 'throw' it instead, and let C# handle the rest.
    float Divide(float a, float b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException();
        }

        return a / b;
    }

    //---
    void GiveUp()
    {
        //It's considered bad practice to throw the base 'Exception' class, instead of a more specific error. 
        throw new Exception("Throwing this doesn't tell anyone what went wrong.");
        //Instead of using Exception, you should [create a new class](https://learn.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions) derived from exception.
    }

    float GetInches(float feet)
    {
        if (feet < 0)
        {
            //ArgumentException is a common exception to throw when putting checks in your code.
            throw new ArgumentException("Negative distances are not allowed.");
        }

        return feet * 12;
    }

    void PlaceholderFunction()
    {
        //NotImplemented is a favorite of mine. I like to write placeholder function names while sketching out ideas for system architecture.
        throw new NotImplementedException("I haven't gotten around to writing this code yet.");
    }
}