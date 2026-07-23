using Microsoft.AspNetCore.Components;
using Radixor.Components.Base;

namespace Radixor.Components.Typography;

public enum CodeSize
{
    Size1,
    Size2,
    Size3,
    Size4,
    Size5,
    Size6,
    Size7,
    Size8,
    Size9
}

public enum CodeVariant
{
    Solid,
    Soft,
    Outline,
    Ghost
}

public enum CodeWeight
{
    Light,
    Regular,
    Medium,
    Bold
}

public partial class Code : SpacingComponentBase
{
    [Parameter] public CodeSize Size { get; set; } = CodeSize.Size2;
    [Parameter] public CodeVariant Variant { get; set; } = CodeVariant.Soft;
    [Parameter] public CodeWeight Weight { get; set; } = CodeWeight.Regular;
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    [Parameter] public bool Truncate { get; set; }
    [Parameter] public bool Wrap { get; set; } = true;
}