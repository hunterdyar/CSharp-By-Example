using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace comment_finder_test;

public class Walker : CSharpSyntaxRewriter
{
	public List<Doc> Comments = new List<Doc>();

	public struct Doc
	{
		public string Comment;
		public Location Location;
	}
	public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
	{
		if (trivia.IsKind(SyntaxKind.EndOfLineTrivia) || trivia.IsKind(SyntaxKind.WhitespaceTrivia))
		{
			return trivia;
		}
		else
		{
			Comments.Add(new Doc()
			{
				Comment = trivia.ToString(),
				Location = trivia.GetLocation(),
			});
			//store a comment and some respective node id.
		}
		//replace trivia with empty.
		return new SyntaxTrivia();
		return base.VisitTrivia(trivia);
	}
}