
using Systems.Collections.Generic;
using System.Linq;
public class ExpressionBodiedExample
{
	//Expression-Bodied properties let you define an implementation in a consise way.
	//They serve as an alternative syntax for getters and reduce the need to write one-line functions.
	public string member => expression;
	//---
	private List<string> _names;
	//They are often used for convenience and simplicity.
	public int Count => _names.Count;
	public bool AnyNames => _names != null && _names.Count > 0;
	//In Unity, one could apply attributes to a private member, like [SerializeField], and use an expression-bodied property for a public getter.
	public List<string> Names => _names;
	
	//Expreession-bodied members work well with LINQ expressions, which can often accomplish a lot of functionality in a single line.
	public int Franks => _names.Where(x => x == "Frank").Count();
	public List<string> GetNamesCapitalized() => _names.Select(n => Char.ToUpperInvariant(n[0]) + n.Substring(1).ToLower()).ToList();
	
}