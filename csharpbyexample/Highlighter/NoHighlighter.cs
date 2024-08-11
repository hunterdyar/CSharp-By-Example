namespace CSharpByExample.Highlighter;

public class NoHighlighter : IHighlighter
{
	public string Highlight(string code)
	{
		return code;
	}
}