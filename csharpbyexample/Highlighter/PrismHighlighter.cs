using System.Reflection;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.Jurassic;
using YamlDotNet.Serialization.BufferedDeserialization;

namespace CSharpByExample.Highlighter;
using JavaScriptEngineSwitcher.Jurassic;

public class PrismHighlighter : IHighlighter
{
	private IJsEngine engine;

	public PrismHighlighter()
	{
		DebugHighlight();

		engine = new JurassicJsEngine();
		Type t = typeof(CSharpByExampleSiteGenerator);
		engine.ExecuteResource("CSharpByExample.Highlighter.prism.js",Assembly.GetAssembly(t));
	}

	public string Highlight(string code)
	{
		
		engine.SetVariableValue("input",code);
		engine.SetVariableValue("lang","csharp");
		engine.Execute($"highlighted = Prism.highlight(input, Prism.languages.csharp, lang)");
		string result = engine.Evaluate<string>("highlighted");

		return result;
	}

	public void DebugHighlight()
	{
		var rootType = typeof(CSharpByExampleSiteGenerator);
		var resourceNames = rootType.Assembly.GetManifestResourceNames();
	}
}