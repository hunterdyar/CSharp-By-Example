// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using CSharpByExample;

public class CSharpByExampleSiteGenerator
{
	public static async Task<int> Main(params string[] args)
	{
		string exampleDir = "./examples";
		string templateDir = "./site/templates";
		string staticFilesDir = "./site/static";
		string buildDir = "./build";

		var rootCommand = new RootCommand("CSharp By Example Runner");
		var examplesDirOption = new Option<DirectoryInfo?>(name: "--examples");
		examplesDirOption.SetDefaultValue(new DirectoryInfo(exampleDir));
		examplesDirOption.AddAlias("e");
		var templatesDirOption = new Option<DirectoryInfo?>(name: "--templates");
		templatesDirOption.AddAlias("t");
		templatesDirOption.SetDefaultValue(new DirectoryInfo(templateDir));
		var staticDirOption = new Option<DirectoryInfo?>(name: "--static");
		staticDirOption.AddAlias("s");
		staticDirOption.SetDefaultValue(new DirectoryInfo(staticFilesDir));
		var buildDirOption = new Option<DirectoryInfo?>(name: "--output");
		buildDirOption.SetDefaultValue(new DirectoryInfo(buildDir));
		buildDirOption.AddAlias("o");
		
		rootCommand.AddOption(examplesDirOption);
		rootCommand.AddOption(templatesDirOption);
		rootCommand.AddOption(staticDirOption);
		rootCommand.AddOption(buildDirOption);

		rootCommand.SetHandler(async (ex, t, s, b) =>
		{
			if (ex != null && t != null && s!= null && b!= null) await Generate(ex, t, s, b);
		},examplesDirOption,templatesDirOption,staticDirOption,buildDirOption);
		
		return await rootCommand.InvokeAsync(args);
	}

	private static async Task Generate(DirectoryInfo exampleDir, DirectoryInfo templateDir, DirectoryInfo staticFilesDir, DirectoryInfo buildDir)
	{
		var site = await SiteParser.Parse(exampleDir.FullName); //end parsing whole site.
		//todo change full name to site.
        Generator g = new Generator(site, templateDir.FullName, staticFilesDir.FullName, buildDir.FullName);
        await g.Generate();

        Console.Write($"completed {site.Examples.Count} examples.");
	}
}