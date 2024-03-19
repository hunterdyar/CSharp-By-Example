namespace CSharpByExample;

public static class SiteParser
{
	public static async Task<SiteDescription> Parse(DirectoryInfo dir)
	{
		SiteDescription site = new SiteDescription();

		foreach (var exampleDir in dir.GetDirectories())
		{
			ExamplePage page = new ExamplePage(exampleDir.Name);
			
			PageParser pageParser = new PageParser(page);
			foreach (var script in exampleDir.GetFiles())
			{
				if (script.Extension == ".yaml")
				{
					await pageParser.Meta(script);
				}
				else
				{
					//todo: check if this is a script or metadata/ignored/image/etc. 
					//wait till we need that to bother implementing it.
					await pageParser.Parse(script.FullName);
				}
			}
			site.Examples.Add(page);
			
		}
		site.ExampleCleanup();
		return site;
	}

	
}