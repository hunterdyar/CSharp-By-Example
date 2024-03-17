// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using CSharpByExample;
using YamlDotNet.Serialization.ObjectGraphTraversalStrategies;

public class CSharpByExampleSiteGenerator
{
	public static async Task<int> Main(params string[] args)
	{
		string exampleDir = "Examples";
		string templateDir = "Templates";
		string staticFilesDir = "static";
		string buildDir = "build";

		var rootCommand = new RootCommand("CSharp By Example Runner");
		var examplesDirOption = new Option<DirectoryInfo?>(name: "--examples");
		examplesDirOption.AddAlias("e");
		var templatesDirOption = new Option<DirectoryInfo?>(name: "--templates");
		templatesDirOption.AddAlias("t");
		var staticDirOption = new Option<DirectoryInfo?>(name: "--static");
		staticDirOption.AddAlias("s");
		var buildDirOption = new Option<DirectoryInfo?>(name: "--output");
		buildDirOption.AddAlias("o");
		
		rootCommand.AddOption(examplesDirOption);
		rootCommand.AddOption(templatesDirOption);
		rootCommand.AddOption(staticDirOption);
		rootCommand.AddOption(buildDirOption);

		rootCommand.SetHandler(async (ex, t, s, b) =>
		{
			await Generate(ex, t, s, b);
		},examplesDirOption,templatesDirOption,staticDirOption,buildDirOption);
		
		return await rootCommand.InvokeAsync(args);
	}

	private static async Task Generate(DirectoryInfo exampleDir, DirectoryInfo templateDir, DirectoryInfo staticFilesDir, DirectoryInfo buildDir)
	{
		var site = await SiteParser.Parse(exampleDir.FullName); //end parsing whole site.
        Generator g = new Generator(site, templateDir.FullName, staticFilesDir.FullName, buildDir.FullName);
        await g.Generate();

        Console.Write($"completed {site.Examples.Count} examples.");
	}
}