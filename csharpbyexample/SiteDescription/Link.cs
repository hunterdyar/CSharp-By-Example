using System.Security.Cryptography;
using YamlDotNet.Serialization;

namespace CSharpByExample;

public class Link
{
	public string URL;
	public string Name;
	
	public Link()
	{
		this.Name = "";
		this.URL = "";
	}
	
	public Link(string url)
	{
		this.Name = url;
		this.URL = url;
	}

	public Link(string name, string url)
	{
		this.Name = name;
		this.URL = url;
	}
}