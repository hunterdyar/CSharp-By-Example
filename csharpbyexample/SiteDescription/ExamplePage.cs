namespace CSharpByExample;

public class ExamplePage : object
{
	//meta doesn't get created if there is no meta file, so we make a default. This allocates some garbage, but.... I don't think I care.
	public PageMeta Meta = new PageMeta();
	
	public string Name = "Test";
	public string ID = "_test";
	public string FileName => ID + "/index.html";
	public ExamplePage NextExample;
	public ExamplePage PrevExample;
	public ExamplePage(string name)
	{
		this.Name = name;
		this.ID = Name.Trim().ToLower().Replace(' ', '-');
	}
	public List<ExampleScript> Scripts = new List<ExampleScript>();
	public void AddScript(ExampleScript exampleScript)
	{
		this.Scripts.Add(exampleScript);
	}
}