using System.Reflection;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.Jurassic;

namespace CSharpByExample.Highlighter;

public class ShikiHighlighter : IHighlighter
{
	private IJsEngine engine;

	public ShikiHighlighter()
	{
		DebugHighlight();

		engine = new JurassicJsEngine();
		Type t = typeof(CSharpByExampleSiteGenerator);
		engine.ExecuteResource("CSharpByExample.Highlighter.shiki.js",Assembly.GetAssembly(t));
	}

	public string Highlight(string code)
	{
		engine.SetVariableValue("input",code);
		engine.SetVariableValue("lang","csharp");
		engine.Execute($"highlighted = Highlight(input)");
		string result = engine.Evaluate<string>("highlighted");

		return result;
	}

	public void DebugHighlight()
	{
		var rootType = typeof(CSharpByExampleSiteGenerator);
		var resourceNames = rootType.Assembly.GetManifestResourceNames();
	}
		
}