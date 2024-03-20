using System;
using System.Collections.Generic;

class Foo
{
    Dictionary<string, string> _someDictionary = new Dictionary<string, string>();
    
    void Main()
    {
        //you can use the keyword 'var' to let C# infer the type.
        foreach (var kvp in _someDictionary)
        {
            Console.WriteLine($"Key: {kvp.Key}, Val: {kvp.Value}");
        }
    }

    //---

    public class Color(float r, float g, float b)
    {
        public float r = r,g = g,b = b;
    }
    

    void ImmutableIterator()
    {
        List<Color> _colors = [new Color(0, 0, 0), new Color(1, 1, 1)];
        foreach (var color in _colors)
        {
            //This won't work! Error!
            //You can't directly modify iterator values. Use a for loop instead.
            color = new Color(.8f, .1f, .4f);
    
            //This will work, because this color iterator variable (that we aren't allowed to assign to) is reference, and we aren't actually changing the color variable.
            //We are following the reference, and then modifying one of it's members.
            color.r = 0;
        }
    
    }
}