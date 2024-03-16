using ColorCode;
using Markdig;
namespace CSharpByExample;

public class ExampleSegment
{
	public string Code;
	public string Doc;
	public bool CodeEmpty;
	public bool IsLeadingSegment;
	public string CodeRendered;
	public string DocsRendered;
	public string css;
	public void Render()
	{
		CodeEmpty = string.IsNullOrEmpty(Code);
		if (!string.IsNullOrEmpty(Doc))
		{
			//todo: cache
			//var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
			//???
			Doc = Doc.Trim();
			//Doc.Replace("\n", "\r\n");
			DocsRendered = Markdown.ToHtml(Doc);
		}
		else
		{
			DocsRendered = "";
		}
		var formatter = new HtmlClassFormatter();

		CodeRendered = formatter.GetHtmlString(Code, Languages.CSharp);
		css = formatter.GetCSSString();

	}
}