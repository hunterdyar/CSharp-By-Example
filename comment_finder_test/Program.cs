// See https://aka.ms/new-console-template for more information

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

Console.WriteLine("Hello, Program!");

var code = new StreamReader("../../../code_to_analyze/test.cs");

SyntaxTree tree = CSharpSyntaxTree.ParseText(code.ReadToEnd());
CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

var comment = root.DescendantTrivia().OfType<SyntaxTrivia>().Where(st=>!st.IsKind(SyntaxKind.WhitespaceTrivia) && !st.IsKind(SyntaxKind.EndOfLineTrivia));
var nodes = root.DescendantNodes(x => true, true).ToList();

var comments = new List<string>();
foreach (var triv in comment)
{
	comments.Add(triv.ToFullString());
}

