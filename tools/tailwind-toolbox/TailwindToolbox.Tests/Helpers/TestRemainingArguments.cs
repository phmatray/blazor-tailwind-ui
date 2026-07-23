using Spectre.Console.Cli;

namespace TailwindToolbox.Tests.Helpers;

/// <summary>
/// Test implementation of IRemainingArguments for use in unit/integration tests.
/// </summary>
internal sealed class TestRemainingArguments : IRemainingArguments
{
    public static TestRemainingArguments Empty { get; } = new(Array.Empty<string>());

    private readonly string[] _raw;

    public TestRemainingArguments(string[] raw)
    {
        _raw = raw;
    }

    public ILookup<string, string?> Parsed => _raw
        .Select((value, index) => new { Key = index.ToString(), Value = (string?)value })
        .ToLookup(x => x.Key, x => x.Value);

    public IReadOnlyList<string> Raw => _raw;
}
