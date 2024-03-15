using ColorCode;

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
		//todo markdown
		DocsRendered = Doc;//markdown rendering.

		var formatter = new HtmlFormatter();

		CodeRendered = formatter.GetHtmlString(Code, Languages.CSharp);
	}
}