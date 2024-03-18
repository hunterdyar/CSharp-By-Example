# CSharp By Example
[https://csharpbyexample.dev](https://csharpbyexample.dev)

Inspired by [gobyexample](https://gobyexample.com/) except re-written from scratch in csharp (obviously). Go By Example is by [Mark McGranaghan](https://github.com/mmcgrana/gobyexample) and this site uses much of the css from it. In addition to being a clone of, you know, it's whole core concept.

I was primarily motivated by my own students. They spend a lot of time watching tutorial videos (including my own) and not enough time simply reading and reverse-engineering code. Having a resource to send them with concise, annotated code is an excellent resource for that refresher. I found GoByExample.com to be very useful as I learned go, and I wished I could send them to csharpbyexample. It didn't exist... and here we are.

## Suggestions
Pull requests are welcome! I am primarily a Unity&C# developer, which can have a different way of doing things than "idiomatic" C#. I am welcome to criticism and change. I know who the site is for - my first-year undergraduate students with a semester's prior programming experience. I believe that it wil also be useful to a lot of other people too!

## How It Works
At a high level, the code first reads through the examples folder, treating each directory as an example. It parses that (see 'Parser' folder) into an intermediary object representation (see 'SiteDescription' folder) using, basically, loops and regex. The script is saved as a list of 'ExampleSegments' that will render as comments/code side-by-side. These segments are converted to HTML using some packages to handle markdown and syntax highlighting. Then, using [Mustache](https://mustache.github.io/) templates, a new folder of html files is written out (see 'Generator' folder). Everything in the 'static' folder gets copied over fresh. A GitHub action handles building the csharp project, running it to generate the site, and then uploading the files to github pages to host.

This is a small project, so I don't bother caching anything, but that's an obvious next step. The project doesn't need to get built when the only changes are the site content.

### Implementation Notes
- It doesn't matter what 'meta' file is called, the code just looks for a file with a 'yaml' extension. If there is more than one, it won't work as intended.
- It doesn't matter what the code file is called, or even that it has a .cs extension, technically. I should probably handle that case eventually.
- If 'Draft: true' is set in the meta YAML, the file is still generated, it's just put in a /drafts/ folder. I intend to use this to get feedback before going live. At the time of writing, everything is still a draft, so it's all just live and pushed to main. That will change once I hit a 'version 1' I am happy with.
- Comments need to appear on their own line to parse correctly. This can be considered a bug.

## Packages
- [Stubble](https://github.com/stubbleorg/stubble) for Mustache template rendering.
- [MarkDig](https://github.com/xoofx/markdig) for Markdown rendering.
- [ColorCode-Universal](https://github.com/CommunityToolkit/ColorCode-Universal) for syntax highlighting.
- [YamlDotNet](https://github.com/aaubry/YamlDotNet/wiki) for YAML file parsing.
- [System.CommandLine](https://github.com/dotnet/command-line-api) (preview) for command line argument parsing.

## License
This work is a derivative of "Go by Example" by [Mark McGranaghan](https://markmcgranaghan.com/), used under [CC BY 3.0](https://creativecommons.org/licenses/by/3.0/).
This work is licensed under [CC BY 4.0](https://creativecommons.org/licenses/by/4.0/)
