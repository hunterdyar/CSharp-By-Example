using System.Security.Cryptography;
using System.Text;
using Stubble.Core.Contexts;
using Stubble.Helpers;

namespace comment_finder_test;
using Stubble.Core.Builders;

public class Generator
{
	private readonly SiteDescription _description;
	private string exampleTemplatePath;
	private string indexTemplatePath;
	private string buildDir = "S:\\csharp\\CSharp-Comments-To-Website\\comment_finder_test\\bin\\Debug\\net6.0\\builds";
	
	public Generator(SiteDescription description, string templateDir)
	{
		exampleTemplatePath = templateDir + "/example.mustache";
		_description = description;
	}

	public async Task Generate()
	{
		//first, copy all the files from static into build
		GenerateIndex();
		foreach (var example in _description.Examples)
		{
			await GenerateExample(example);
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
		helpers.Register("Name", (context) => context.Lookup<ExamplePage>("Name").Name);
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
		using (StreamReader streamReader = new StreamReader(exampleTemplatePath, Encoding.UTF8))
		{
			var content = await streamReader.ReadToEndAsync();
			var output = await stubble.RenderAsync(content, examplePage);
			
			//todo: save output to /examples
			File.WriteAllTextAsync(buildDir+"/"+examplePage.Name+".html",output);
		}
	}
	
}