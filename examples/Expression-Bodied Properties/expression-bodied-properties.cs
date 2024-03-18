//Expression-Bodied properties let you define an implementation in a consise way. 
//They serve as an alternative syntax for getters, and reduct the number of one-line functions you may write.
using Systems.Collections.Generic;
using System.Linq;
public class ExpressionBodiedExample
{
	
	public string member => expression;
	
	//
	
	//_name is private get/set, and Names is public get-only. This is a convenient pattern. 
	//In Unity, now one could apply attributes too the private member, like [SerializeField].
	private List<string> _names;
	public List<string> Names => _names;
	public int Count => _names.Count;
	
	//Expreession-bodied members work well with LINQ expressions, which can often accomplish a lot of functionality in a single expression.  
	//See also: LINQ
	public int Franks => _names.Where(x => x == "Frank").Count();
	public List<string> GetNamesCapitalized() => _names.Select(n => Char.ToUpperInvariant(n[0]) + n.Substring(1).ToLower()).ToList();

	//this is another way to accomplish a public-getter private-setter approach. I find it more difficult to intuit about.
	public List<string> OldNames; {get private set; }
	
	//
//  

}