using CSharpByExample;
using YamlDotNet.Serialization;

public class CSharpByExampleSiteGenerator
{
	public static async Task<int> Main(params string[] args)
	{
		string defaultSettingsFile = "./settings.yaml";
		FileInfo settingsFile;
		if (args.Length == 0)
		{
			settingsFile = new FileInfo(defaultSettingsFile);
		}
		else
		{
			settingsFile = new FileInfo(args[0]);
		}
		
		return await Generate(settingsFile);
	}

	private static async Task<int> Generate(FileInfo settings)
	{
		SiteSettings siteSettings;
		using (StreamReader sr = new StreamReader(settings.FullName))
		{
			var yaml = await sr.ReadToEndAsync();
			var deserializer = new DeserializerBuilder()
				.WithTypeInspector(n=>new IgnoreCaseTypeInspector(n))
				.Build();
			siteSettings = deserializer.Deserialize<SiteSettings>(yaml);
		}

		siteSettings.GetDirectoryInfo();

		var site = await SiteParser.Parse(siteSettings.ExampleDirInfo); //end parsing whole site.
        Generator g = new Generator(site, siteSettings.TemplateDirInfo, siteSettings.StaticBuildInfo, siteSettings.BuildDirInfo);
        await g.Generate();

        Console.Write($"completed {site.Examples.Count} examples.");
        return 0;
	}
}