using YamlDotNet.Serialization;

namespace CSharpByExample;

public class PageMeta
{
	public int Order = 9001;//default is later order
	public bool Draft = false;
	public bool HasLinks => Links.Length > 0;//this is just a convenience for the mustache template.
	
	public Link[] Links = Array.Empty<Link>();
}