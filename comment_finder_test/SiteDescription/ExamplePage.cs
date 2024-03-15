﻿namespace comment_finder_test;

public class ExamplePage : object
{
	public string Name = "Test";
	public string ID = "_test";
	public string FileName => ID + ".html";
	public ExamplePage NextExample;
	public ExamplePage PrevExample;
	
	public List<ExampleScript> Scripts = new List<ExampleScript>();
	public void AddScript(ExampleScript exampleScript)
	{
		this.Scripts.Add(exampleScript);
	}
}