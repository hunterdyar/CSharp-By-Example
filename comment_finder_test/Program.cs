// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using comment_finder_test;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

string file = "../../../code_to_analyze/test.cs";
//var comments = root.DescendantTrivia().OfType<SyntaxTrivia>().Where(st=>!st.IsKind(SyntaxKind.WhitespaceTrivia) && !st.IsKind(SyntaxKind.EndOfLineTrivia)).ToList();
//var nodes = root.DescendantNodes(x => true, true).ToList();

//paring
var site = new SiteDescription();
//foreach example in code directory...
var page = new ExamplePage();
Parser p = new Parser(page);
//foreach script in example directory
await p.Parse(file);
//end script
site.Examples.Add(page);
//end example

site.SetNextPrevious();
//end parsing whole site.

Generator g = new Generator(site,"../../../Templates/","../../../static","../../../build");
await g.Generate();

Console.Write($"completed {p.Page.Scripts.Count} examples.");
//the main challenge is that using CodeAnalysis gives us a tree walker to use, but separating our 'docs' from the code will require more linear, less recursive approach.
//luckily, extending TreeWalker gives us a walk in the correct order. So we need to walk the tree, and for every node that is a comment, we can add it to a docs list, with a reference to it's associated token.
//then, we render the code and render the comments.... and somehow find that relevant token to figure out what group or node we belong to? I think?


//Hmmm, looks like maybe copying the comments out using TextSpan into docs might be the way to go.
//or the way that gobyexample does it, which is to not use the SyntaxTree and, instead, to use regex.
//which would, honestly, be fine. Any edge-cases we can test and re-write.