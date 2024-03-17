﻿using Stubble.Core.Renderers;

namespace CSharpByExample;

public class ExampleScript : object
{
	public List<ExampleSegment> Segments = new List<ExampleSegment>();
	public string file;

	public ExampleScript(List<ExampleSegment> segments, string scriptFile)
	{
		Segments = segments;
		file = scriptFile;
	}
}