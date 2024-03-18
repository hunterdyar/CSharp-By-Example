using ColorCode;
using Markdig;
namespace CSharpByExample;

public class ExampleSegment
{
	public string Code = "";
	public string Doc = "";
	public bool CodeEmpty;
	public bool IsLeadingSegment;
	public string CodeRendered = "";
	public string DocsRendered = "";
	public void Render()
	{
		CodeEmpty = string.IsNullOrEmpty(Code);
		if (!string.IsNullOrEmpty(Doc))
		{
			Doc = Doc.Trim();
			DocsRendered = Markdown.ToHtml(Doc);
		}
		else
		{
			DocsRendered = "";
		}
		var formatter = new HtmlClassFormatter();

		CodeRendered = formatter.GetHtmlString(Code, Languages.CSharp);
	}
}