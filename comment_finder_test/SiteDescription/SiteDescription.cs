namespace comment_finder_test;

public class SiteDescription
{
	public string SiteName;
	public List<ExamplePage> Examples = new List<ExamplePage>();

	public void SetNextPrevious()
	{
		for (var i = 0; i < Examples.Count; i++)
		{
			var s = Examples[i];
			if (i < Examples.Count - 1)
			{
				s.NextExample = Examples[i + 1];
			}

			if (i > 0)
			{
				s.PrevExample = Examples[i - 1];
			}
		}
	}
}