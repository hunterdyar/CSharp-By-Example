using System.Security.Cryptography;
using System.Text;
using Stubble.Core.Contexts;
using Stubble.Helpers;

namespace comment_finder_test;
using Stubble.Core.Builders;

public class Generator
{
	private readonly SiteDescription _description;
	private readonly DirectoryInfo _templateDir;
	private readonly DirectoryInfo _buildDir;
	private readonly DirectoryInfo _staticDir;
	private string IndexFile => Path.Join(_buildDir.FullName, "/index.html");
	public Generator(SiteDescription description, string templateDir, string staticDir, string buildDir)
	{
		this._buildDir = new DirectoryInfo(buildDir);
		this._staticDir = new DirectoryInfo(staticDir);
		string exampleTemplatePath = templateDir;
		_templateDir = new DirectoryInfo(exampleTemplatePath);
		
		_description = description;
	}

	private string GetFilePath(ExamplePage page)
	{
		return Path.Join(_buildDir.FullName, page.ID + ".html");
	}
	
	public async Task Generate()
	{
		//first, copy all the files from static into build
		ClearFiles();
		CopyStaticToBuild();
		GenerateIndex();
		foreach (var example in _description.Examples)
		{
			await GenerateExample(example);
		}
	}

	private void ClearFiles()
	{
		if (File.Exists(IndexFile))
		{
			File.Delete(IndexFile);
		}
		foreach(var example in _description.Examples)
		{
			var path = GetFilePath(example);
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}
	}

	private void CopyStaticToBuild()
	{
		foreach (var sFile in _staticDir.GetFiles())
		{
			//we already deleted the destFile
			var destFile = Path.Join(_buildDir.FullName, sFile.Name);
			Directory.Move(sFile.FullName, destFile);
		}	
	}

	async Task GenerateIndex()
	{ var stubble = new StubbleBuilder().Build();
		
    	using (StreamReader streamReader = new StreamReader(_templateDir+"/index.mustache", Encoding.UTF8))
    	{
    		string content = await streamReader.ReadToEndAsync();
    		string? output = await stubble.RenderAsync(content, _description);
            await File.WriteAllTextAsync(IndexFile, output);

        }	
	}

	public Helpers GetHelpers()
	{
		var helpers = new Helpers();
		//helpers.Register("Name", (context) => context.Lookup<ExamplePage>("Name").Name);
		return helpers;
	}
	//Generate a single example
	public async Task GenerateExample(ExamplePage examplePage)
	{
		var helpers = GetHelpers();
		var stubble = new StubbleBuilder()
			.Configure(conf=>conf.AddHelpers(helpers))
			.Build();
		
		
		//obj = 
		using (StreamReader streamReader = new StreamReader(_templateDir+"/example.mustache", Encoding.UTF8))
		{
			var obj = examplePage;
			var content = await streamReader.ReadToEndAsync();
			var output = await stubble.RenderAsync(content, examplePage);
			await File.WriteAllTextAsync(GetFilePath(examplePage),output);
		}
	}
	
}