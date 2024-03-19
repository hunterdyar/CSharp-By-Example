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
	public void GetDirectoryInfo()
	{
		BuildDirInfo = new DirectoryInfo(BuildDir);
		StaticBuildInfo = new DirectoryInfo(StaticBuildDir);
		ExampleDirInfo = new DirectoryInfo(ExampleDir);
		TemplateDirInfo = new DirectoryInfo(TemplateDir);
		
		//validate?
	}
}