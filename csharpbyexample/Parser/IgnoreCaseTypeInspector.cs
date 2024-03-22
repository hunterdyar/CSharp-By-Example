using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using YamlDotNet.Serialization;

namespace CSharpByExample;

//https://github.com/aaubry/YamlDotNet/issues/527#issuecomment-690741088
public class IgnoreCaseTypeInspector : ITypeInspector
{
	private readonly ITypeInspector innerTypeInspector;

	public IgnoreCaseTypeInspector(ITypeInspector innerTypeInspector)
	{
		this.innerTypeInspector = innerTypeInspector ?? throw new ArgumentNullException(nameof(innerTypeInspector));
	}

	public IEnumerable<IPropertyDescriptor> GetProperties(Type type, object? container)
	{
		return innerTypeInspector.GetProperties(type, container);
	}

	public IPropertyDescriptor GetProperty(Type type, object? container, string name,
		[MaybeNullWhen(true)] bool ignoreUnmatched)
	{
		var candidates = GetProperties(type, container)
			.Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

		using (var enumerator = candidates.GetEnumerator())
		{
			if (!enumerator.MoveNext())
			{
				if (ignoreUnmatched)
				{
					return null!;
				}

				throw new SerializationException($"Property '{name}' not found on type '{type.FullName}'.");
			}

			var property = enumerator.Current;

			if (enumerator.MoveNext())
			{
				throw new SerializationException(
					$"Multiple properties with the name/alias '{name}' already exists on type '{type.FullName}', maybe you're misusing YamlAlias or maybe you are using the wrong naming convention? The matching properties are: {string.Join(", ", candidates.Select(p => p.Name).ToArray())}"
				);
			}

			return property;
		}
	}
}