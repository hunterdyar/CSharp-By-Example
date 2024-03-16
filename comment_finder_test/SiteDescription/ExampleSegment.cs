using ColorCode;
using Markdig;

namespace comment_finder_test;

public class ExampleSegment
{
	public string Code;
	public string Doc;
	public bool CodeEmpty => string.IsNullOrEmpty(Code);
	public bool IsLeadingSegment;
	public string CodeRendered;
	public string DocsRendered;

	public void Render()
	{
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

		var formatter = new HtmlFormatter();

		CodeRendered = formatter.GetHtmlString(Code, Languages.CSharp);
		
	}
}