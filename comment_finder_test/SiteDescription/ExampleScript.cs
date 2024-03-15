using Stubble.Core.Renderers;

namespace comment_finder_test;

public class ExampleScript : object
{
	public List<ExampleSegment> Segments = new List<ExampleSegment>();
	public string file;

	public ExampleScript(List<ExampleSegment> segments, string scriptFile)
	{
		Segments = segments;
		file = scriptFile;

		for (var i = 0; i < segments.Count; i++)
		{
			var s = segments[i];
			if (i < segments.Count-1)
			{
				s.NextExample = segments[i + 1];
			}

			if (i > 0)
			{
				s.PrevExample = segments[i - 1];
			}
		}
	}
}
