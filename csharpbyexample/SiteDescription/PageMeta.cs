namespace CSharpByExample;

public class PageMeta
{
	public int Order = 9001;//default is later order
	public bool HasLinks => Links != null && Links.Length > 0;//this is just a convenience for the mustache template.
	public string[] Links;
}