using System.Text;
using Stubble.Core.Loaders;
using Stubble.Core.Builders;

namespace CSharpByExample;

public class Generator
{
	private readonly SiteDescription _description;
	private readonly DirectoryInfo _templateDir;
	private readonly DirectoryInfo _buildDir;
	private readonly DirectoryInfo _staticDir;
	private readonly DirectoryInfo _draftsDir;
	private DictionaryLoader _partialsLoader;
	private string IndexFile => Path.Join(_buildDir.FullName, "/index.html");
	public Generator(SiteDescription description, string templateDir, string staticDir, string buildDir)
	{
		this._buildDir = new DirectoryInfo(buildDir);
		this._draftsDir = new DirectoryInfo(Path.Join(_buildDir.FullName, "/drafts"));
		this._staticDir = new DirectoryInfo(staticDir);
		string exampleTemplatePath = templateDir;
		_templateDir = new DirectoryInfo(exampleTemplatePath);
		_description = description;
	}

	private string GetExampleFilePath(ExamplePage page, bool createDir = true)
	{
		var dirInfo = page.Meta.Draft ? _draftsDir : _buildDir;
		string dir = Path.Join(dirInfo.FullName, page.ID);
		if (createDir)
		{
			Directory.CreateDirectory(dir);
		}
		return Path.Join(dir + "/index.html");
	}
	
	public async Task Generate()
	{
		//Init state
		_partialsLoader = await CreatePartialsLoader();
		InitBuildDir();
		
		//Generate
		CopyStaticToBuild();
		await GenerateIndex();
		foreach (var example in _description.Examples)
		{
			await GenerateExample(example);
		}
	}

	private void InitBuildDir()
	{
		if (!Directory.Exists(_buildDir.FullName))
		{
			Directory.CreateDirectory(_buildDir.FullName);
			Directory.CreateDirectory(_draftsDir.FullName);
		}
		else
		{
			ClearDirectories(_buildDir, false);
		}
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
	{ 
		var stubble = new StubbleBuilder()
			.Configure(conf=>conf.AddToPartialTemplateLoader(_partialsLoader))
			.Build();
		
    	using (StreamReader streamReader = new StreamReader(_templateDir+"/index.mustache", Encoding.UTF8))
    	{
    		string content = await streamReader.ReadToEndAsync();
    		string? output = await stubble.RenderAsync(content, _description);
            await File.WriteAllTextAsync(IndexFile, output);
        }	
	}

	private async Task<DictionaryLoader> CreatePartialsLoader()
	{
		var partials = new Dictionary<string, string>();

		foreach (var file in _templateDir.GetFiles())
		{
			if (file.Name == "index.mustache" || file.Name == "example.mustache")
			{
				continue;
			}

			var stream = new StreamReader(file.FullName);
			partials.Add(Path.GetFileNameWithoutExtension(file.Name),await stream.ReadToEndAsync());
		}
		
		return new DictionaryLoader(partials);
	}
	//Generate a single example
	public async Task GenerateExample(ExamplePage examplePage)
	{
		var stubble = new StubbleBuilder()
			.Configure(conf=>conf.AddToPartialTemplateLoader(_partialsLoader))
			.Build();
		
		using (StreamReader streamReader = new StreamReader(_templateDir+"/example.mustache", Encoding.UTF8))
		{
			var content = await streamReader.ReadToEndAsync();
			var output = await stubble.RenderAsync(content, examplePage);
			await File.WriteAllTextAsync(GetExampleFilePath(examplePage),output);
		}
	}
	
}