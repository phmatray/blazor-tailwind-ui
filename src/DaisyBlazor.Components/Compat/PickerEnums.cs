namespace DaisyBlazor;

// MudBlazor-compatible picker enums so consuming markup migrates with only a tag rename.
// The daisyUI replacements render native inputs, so most members are accepted but not visually
// distinct; they exist purely to keep existing attribute bindings compiling.

/// <summary>Picker presentation mode (MudBlazor compat).</summary>
public enum PickerVariant
{
    Inline,
    Static,
    Dialog
}

/// <summary>Color picker view mode (MudBlazor compat).</summary>
public enum ColorPickerView
{
    Spectrum,
    Grid,
    GridCompact,
    Palette
}
