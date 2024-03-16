namespace comment_finder_test;

public static class SiteParser
{
	public static async Task<SiteDescription> Parse(string examplesDir)
	{
		SiteDescription site = new SiteDescription();
		DirectoryInfo dir = new DirectoryInfo(examplesDir);

		foreach (var exampleDir in dir.GetDirectories())
		{
			//todo: examplePage could contain it's parser. Why not! OO prinicples involves organizing data with the methods that operate on them.
			ExamplePage page = new ExamplePage(exampleDir.Name);
			
			PageParser pageParser = new PageParser(page);
			foreach (var script in exampleDir.GetFiles())
			{
				//todo: check if this is a script of metadata/ignored, via some syntax of some kind? 
				//wait till we need that to bother implementing it.
				await pageParser.Parse(script.FullName);
			}
			site.Examples.Add(page);
		}
		return site;
	}

	
}