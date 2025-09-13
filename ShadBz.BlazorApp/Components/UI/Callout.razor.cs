using Microsoft.AspNetCore.Components;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public enum CalloutSize
{
    Size1,
    Size2,
    Size3
}

public enum CalloutVariant
{
    Soft,
    Surface,
    Outline
}

public partial class Callout : SpacingComponentBase
{
    [Parameter] public CalloutSize Size { get; set; } = CalloutSize.Size2;
    [Parameter] public CalloutVariant Variant { get; set; } = CalloutVariant.Soft;
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool HighContrast { get; set; }
}