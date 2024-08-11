using CSharpByExample.Highlighter;

namespace CSharpByExample;

public struct SiteSettings
{
	public string Name;
	public DirectoryInfo ExampleDirInfo;
	public string ExampleDir;
	public DirectoryInfo StaticBuildInfo;
	public string StaticBuildDir;
	public DirectoryInfo BuildDirInfo;
	public string BuildDir;
	public DirectoryInfo TemplateDirInfo;
	public string TemplateDir;
	public string Highlighter;
	public void GetDirectoryInfo()
	{
		BuildDirInfo = new DirectoryInfo(BuildDir);
		StaticBuildInfo = new DirectoryInfo(StaticBuildDir);
		ExampleDirInfo = new DirectoryInfo(ExampleDir);
		TemplateDirInfo = new DirectoryInfo(TemplateDir);
		
		//validate?
	}

	public IHighlighter GetHighlighter()
	{
		var h = Highlighter.ToLower().Trim();
		if (h == "no" || string.IsNullOrEmpty(Highlighter))
		{
			return new NoHighlighter();
		}else if (h == "prism")
		{
			return new PrismHighlighter();
		}else if (h == "shiki")
		{
			return new ShikiHighlighter();
		}

		throw new ArgumentException($"Invalid Highlighter setting: {Highlighter}");
	}
}