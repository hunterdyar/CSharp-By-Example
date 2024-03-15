using System.Text.RegularExpressions;

namespace comment_finder_test;

public class Parser
{
	public ExamplePage Page => _page;
	private ExamplePage _page;

	private Regex blockCommentPattern = new Regex(@"\/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+\/");
	private Regex singleLineCommentPattern = new Regex(@"(\/\/).*");
	private Regex selectDoubleSlash = new Regex("^\t*(/)(/)");
	private Regex dash = new Regex("/-+");
	
	//Parses a script into an ExampleScript and adds it to a given Example Page.
	public Parser(ExamplePage page)
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
				///split by /n, but need to remove /r.
				string line = l.TrimEnd();	
				if (line == "")
				{
					continue;
				}
				
				var matchDocs = singleLineCommentPattern.Match(line).Success || blockCommentPattern.Match(line).Success; 
				var matchCode = !matchDocs;

				//it's a new stretch of comments if the previous thing was not code, or if the current segment is empty.
				bool startDoc = lastSeen != SegmentType.Doc ||
				                segments.Count == 0 || string.IsNullOrEmpty(segments[^1].Doc);
				bool startCode = lastSeen != SegmentType.Code || segments.Count == 0 ||
				                 string.IsNullOrEmpty(segments[^1].Code);
				
				//if it's a comment
				if (matchDocs)
				{
					var trimmed = selectDoubleSlash.Replace(line, "");
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
						segment.Doc += '\n' + trimmed;
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
						segment.Code += segment.Code.Length != 0 ?  "/n" : "";
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

		_page.AddScript(new ExampleScript(segments,scriptFile));
		
	}
}