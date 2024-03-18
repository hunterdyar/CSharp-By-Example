int a, b, c;
//Arithmetic operators behave like you expect.
a = 42;
b = a;
c = a-b;
c = a+b;
b++;
//C# does not have a power/exponent operator (^ on calculators, ** in python). Use [Math.Pow](https://learn.microsoft.com/en-us/dotnet/api/system.math.pow?view=net-8.0&redirectedfrom=MSDN#System_Math_Pow_System_Double_System_Double_).
c = Math.Pow(a,b);
//Order of operations (PEMDOS) is respected.
a = b+c*a;
//An integer divided by an integer will return an integer. You may lose precision without fractions.
a = b/c;

//If a float is part of division, then a float (with fractions) is returned.  
//This is true for double's too.
float v = b/(float)c;

//

//Comparison operators return boolean values.
bool result = a > b;
result = a >= b;

//The bang operator (!) inverts a bool.
result = !result;
bool thisIsTrue = !false;

//Boolean operators compare booleans.  
//&& is *and*  
//|| is *or*  
//^ is *exclusive or*  
bool bothMustBeTrue = result && thisIsTrue;

//

//
bool DoesThisExecute()
{
    Console.Write("Will this message appear?");
    return true;
}
//The && and || operators do not execute the right hand side if it is uneccessary.  
//In complex comparisons, put the faster-to-calculate elements first.
result == false && DoesThisExecute();
result == true || DoesThisExecute(); 


//The single & and | also are 'and' and 'or', respectively, but they will always execute both sides of the operation.
result == false & DoesThisExecute();
result == true | DoesThisExecute();

//As a rule-of-thumb, use && and || over & and | until you hit a situation where you need them.  
//This is not just a slight performance improvement, but clarity: & and | are also used for [bitwise operations](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators).