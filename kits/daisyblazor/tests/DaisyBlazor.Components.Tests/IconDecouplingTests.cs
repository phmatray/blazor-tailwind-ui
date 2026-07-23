using DaisyBlazor;

namespace DaisyBlazor.Components.Tests;

/// <summary>
/// Guards that the Material Symbols font dependency is opt-out, not forced:
/// the icon-font class is swappable via a cascaded <see cref="IconOptions"/>, and
/// <see cref="Icon"/> accepts inline-SVG child content as an escape hatch.
/// </summary>
public class IconDecouplingTests : BunitContext
{
    [Fact]
    public void Icon_defaults_to_material_symbols_font_class()
    {
        IRenderedComponent<Icon> cut = Render<Icon>(ps => ps.Add(p => p.Name, "home"));

        cut.Find("span").ClassList.ShouldContain("material-symbols-outlined");
        cut.Markup.ShouldContain("home");
    }

    [Fact]
    public void Icon_uses_cascaded_font_class()
    {
        IRenderedComponent<Icon> cut = Render<Icon>(ps => ps
            .AddCascadingValue(new IconOptions { FontClass = "lucide-font" })
            .Add(p => p.Name, "home"));

        cut.Find("span").ClassList.ShouldContain("lucide-font");
        cut.Markup.ShouldNotContain("material-symbols-outlined");
    }

    [Fact]
    public void Icon_renders_child_content_as_inline_svg_escape()
    {
        IRenderedComponent<Icon> cut = Render<Icon>(ps => ps
            .AddChildContent("<svg class=\"probe-svg\"></svg>"));

        cut.Markup.ShouldContain("probe-svg");
        cut.Markup.ShouldNotContain("material-symbols-outlined");
    }

    [Fact]
    public void Icon_child_content_takes_precedence_over_name()
    {
        IRenderedComponent<Icon> cut = Render<Icon>(ps => ps
            .Add(p => p.Name, "home")
            .AddChildContent("<svg class=\"probe-svg\"></svg>"));

        cut.Markup.ShouldContain("probe-svg");
        cut.Markup.ShouldNotContain("material-symbols-outlined");
    }

    [Fact]
    public void Cascaded_font_class_propagates_to_icons_nested_in_composites()
    {
        // Alert renders a nested <Icon> for its severity glyph; the cascade must reach it,
        // which is the whole value of cascading once at the app root.
        IRenderedComponent<Alert> cut = Render<Alert>(ps => ps
            .AddCascadingValue(new IconOptions { FontClass = "material-symbols-rounded" })
            .Add(p => p.Severity, Severity.Info)
            .AddChildContent("heads up"));

        cut.Markup.ShouldContain("material-symbols-rounded");
        cut.Markup.ShouldNotContain("material-symbols-outlined");
    }
}
