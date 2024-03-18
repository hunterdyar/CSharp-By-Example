namespace CSharpByExample;

public class ExampleScript : object
{
	public List<ExampleSegment> Segments = new List<ExampleSegment>();
	public string File;

	public ExampleScript(List<ExampleSegment> segments, string scriptFile)
	{
		Segments = segments;
		File = scriptFile;
	}
}
