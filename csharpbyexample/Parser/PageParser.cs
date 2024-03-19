using System.Text.RegularExpressions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CSharpByExample;

public class PageParser
{
	public ExamplePage Page => _page;
	private ExamplePage _page;
	
	
	private Regex _blockCommentPattern = new Regex(@"\/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+\/");
	private Regex _singleLineCommentPattern = new Regex(@"(\/\/).*");
	private Regex _selectDoubleSlash = new Regex("^\t* *(/)(/)");

	public bool ReplaceTabsWithSpaces = true;

	//Parses a script into an ExampleScript and adds it to a given Example Page.
	public PageParser(ExamplePage page)
	{
		_page = page;
	}

	public async Task Parse(string scriptFile)
	{
		//translating this from the gobyexample code first, making it "c#-ey" later.
		List<ExampleSegment> segments = new List<ExampleSegment>();
		using (StreamReader sr = new StreamReader(scriptFile))
		{
			var script = await sr.ReadToEndAsync();
			SegmentType lastSeen = SegmentType.Nothing;
			foreach (string l in script.Split('\n'))
			{
				//windows does carriage-return+newline; unix does just newline.
				string line = l.Replace("\r","");//don't trim end, we use spaces on a new line to force p breaks.
				if (ReplaceTabsWithSpaces)
				{
					//tabs may not render at consistent widths on different browsers/html renderers.
					line = line.Replace("\t", "    ");
				}
				if (line == "")
				{
					continue;
				}else if(line.Contains("//---"))
				{
					//This forces an empty block. It would be better if we did it manually, that's an 'eventually' todo.
					segments.Add(new ExampleSegment());
					segments.Add(new ExampleSegment());	
					//skip to next!
					continue;
				}
					
				var matchDocs = _singleLineCommentPattern.Match(line).Success || _blockCommentPattern.Match(line).Success; 
				//var matchCode = !matchDocs;

				//it's a new stretch of comments if the previous thing was not code, or if the current segment is empty.
				bool startDoc = lastSeen != SegmentType.Doc ||
				                segments.Count == 0 || string.IsNullOrEmpty(segments[^1].Doc);
				bool startCode = lastSeen != SegmentType.Code || segments.Count == 0 ||
				                 string.IsNullOrEmpty(segments[^1].Code);
				
				//if it's a comment
				if (matchDocs)
				{
					var trimmed = _selectDoubleSlash.Replace(line, "");
					if (startDoc)
					{
						var s = new ExampleSegment();
						s.Doc = trimmed;
						s.Code = "";
						segments.Add(s);
					}
					else
					{
						var segment = segments[^1];
						segment.Doc += '\n' + trimmed;//We could add "|n  " (with spaces) to have the markdown renderer force new lines.... but this should be user controlled....
					}

					lastSeen = SegmentType.Doc;
				}
				else //is code
				{
					if (startCode)
					{
						if (segments.Count == 0)
						{
							segments.Add(new ExampleSegment());
						}
						segments[^1].Code = line;
					}
					else
					{
						var segment = segments[^1];
						segment.Code += segment.Code.Length != 0 ?  "\n" : "";
						segment.Code += line;
					}

					lastSeen = SegmentType.Code;
				}
			}

			for (var i = 0; i < segments.Count; i++)
			{
				var segment = segments[i];
				segment.IsLeadingSegment = i < segments.Count - 1;
			}
		}

		foreach (var segment in segments)
		{	
			segment.Render();
		}
		_page.AddScript(new ExampleScript(segments,scriptFile));
		//done! This class is basically a wraper for this function. We could just have a static function that returns the page.
		//That parser is used on the SiteParser. The inconsistency could be considered bad, but I'll say this is demonstrating techniques as an educational exercise. Sure.
	}

	public async Task Meta(FileInfo yamlFileInfo)
	{
		using (StreamReader sr = new StreamReader(yamlFileInfo.FullName))
		{
			var yaml = await sr.ReadToEndAsync();
			var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance)
				.WithNamingConvention(PascalCaseNamingConvention.Instance).Build();
			_page.Meta = deserializer.Deserialize<PageMeta>(yaml);
			
		}
	}
}