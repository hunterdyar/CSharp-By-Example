using System.Security.Cryptography;
using System.Text;
using Stubble.Core.Contexts;
using Stubble.Helpers;

namespace comment_finder_test;
using Stubble.Core.Builders;

public class Generator
{
	private readonly SiteDescription _description;
	private DirectoryInfo exampleTemplateDir;
	private string indexTemplatePath;
	private DirectoryInfo buildDir;
	public Generator(SiteDescription description, string templateDir, string buildDir)
	{
		this.buildDir = new DirectoryInfo(buildDir);

		string exampleTemplatePath = templateDir;
		exampleTemplateDir = new DirectoryInfo(exampleTemplatePath);
		
		_description = description;
	}

	public async Task Generate()
	{
		//first, copy all the files from static into build
		CopyStaticToBuild();
		GenerateIndex();
		foreach (var example in _description.Examples)
		{
			await GenerateExample(example);
		}
	}

	private void CopyStaticToBuild()
	{
		foreach (var sFile in buildDir.GetFiles())
		{
			Directory.Move(sFile.FullName, buildDir + sFile.Name);
		}	
	}

	async Task GenerateIndex()
	{ var stubble = new StubbleBuilder().Build();
		
    	using (StreamReader streamReader = new StreamReader(indexTemplatePath, Encoding.UTF8))
    	{
    		string content = await streamReader.ReadToEndAsync();
    		string? output = await stubble.RenderAsync(content, _description);
    		//todo: save output to /index
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
		using (StreamReader streamReader = new StreamReader(exampleTemplateDir+"/example.mustache", Encoding.UTF8))
		{
			var obj = examplePage;
			var content = await streamReader.ReadToEndAsync();
			var output = await stubble.RenderAsync(content, obj);

			
			//todo: save output to /examples
			File.WriteAllTextAsync(buildDir+"/"+examplePage.Name+".html",output);
		}
	}
	
}