namespace comment_finder_test;

public class ExampleSegment
{
	public string Code;
	public string Doc;
	public bool CodeEmpty => string.IsNullOrEmpty(Code);
	public bool IsLeadingSegment;
	public string CodeRendered => Code;
	public string DocsRendered => Doc;
	public ExampleSegment NextExample;
	public ExampleSegment PrevExample;
}