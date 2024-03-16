using System.Text;
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

	private string GetExampleFilePath(ExamplePage page, bool createDir = true)
	{
		string dir = Path.Join(_buildDir.FullName, page.ID);
		if (createDir)
		{
			Directory.CreateDirectory(dir);
		}
		return Path.Join(dir + "/index.html");
	}
	
	public async Task Generate()
	{
		//first, copy all the files from static into build
		ClearFiles();
		CopyStaticToBuild();
		await GenerateIndex();
		foreach (var example in _description.Examples)
		{
			await GenerateExample(example);
		}
	}

	private void ClearFiles()
	{
		ClearDirectories(_buildDir,false);	
	}

	void ClearDirectories(DirectoryInfo dir,bool deleteDir=true)
	{
		foreach (var d in dir.GetDirectories())
		{
			ClearDirectories(d);
		}

		foreach (var f in dir.GetFiles())
		{
			File.Delete(f.FullName);	
		}

		if (deleteDir)
		{
			Directory.Delete(dir.FullName);
		}
	}

	private void CopyStaticToBuild()
	{
		if (_staticDir.Exists)
		{
			foreach (var sFile in _staticDir.GetFiles())
			{
				//todo: this won't copy sub-folders
				//we already deleted the destFile
				var destFile = Path.Join(_buildDir.FullName, sFile.Name);
				File.Copy(sFile.FullName, destFile);
			}
		}
		else
		{
			//if we didn't find one but we anticipated finding one, warn us.
			if (_staticDir.Name != "")
			{
				Console.WriteLine("No static directory to copy folders from. ");
			}
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
	private Dictionary<string,string> GetPartials()
	{
		var partials = new Dictionary<string, string>();

		partials.Add("footer","footer.mustache");
		
		return partials;
	}
	//Generate a single example
	public async Task GenerateExample(ExamplePage examplePage)
	{
		var helpers = GetHelpers();
		var stubble = new StubbleBuilder()
			.Configure(conf=>conf.AddHelpers(helpers))
			.Build();
		
		using (StreamReader streamReader = new StreamReader(_templateDir+"/example.mustache", Encoding.UTF8))
		{
			var content = await streamReader.ReadToEndAsync();
			var output = await stubble.RenderAsync(content, examplePage);
			await File.WriteAllTextAsync(GetExampleFilePath(examplePage),output);
		}
	}
	
}