using DaisyBlazor;
using DaisyBlazor.Data;
using Microsoft.Extensions.Localization;

namespace DaisyBlazor.Components.Tests;

/// <summary>
/// Guards that theme-coupled components fall back to daisyUI theme tokens instead of
/// hardcoded brand colors when no explicit value is supplied, while still honouring an
/// explicit value when one is passed (backward-compatible).
/// </summary>
public class ThemeDefaultsTests : BunitContext
{
    [Fact]
    public void FeatureHero_without_gradient_params_emits_no_hardcoded_colors()
    {
        IRenderedComponent<FeatureHero> cut = Render<FeatureHero>(ps => ps
            .Add(p => p.Icon, "home")
            .Add(p => p.Title, "Operations"));

        // No inline gradient custom-properties => the scoped CSS theme-token fallback wins.
        cut.Markup.ShouldNotContain("--hero-start");
        cut.Markup.ShouldNotContain("--hero-end");
        cut.Markup.ShouldNotContain("#1976d2");
        cut.Markup.ShouldNotContain("#42a5f5");
    }

    [Fact]
    public void FeatureHero_with_gradient_params_still_emits_inline_colors()
    {
        IRenderedComponent<FeatureHero> cut = Render<FeatureHero>(ps => ps
            .Add(p => p.Icon, "home")
            .Add(p => p.Title, "Operations")
            .Add(p => p.GradientStart, "#aabbcc")
            .Add(p => p.GradientEnd, "#ddeeff"));

        cut.Markup.ShouldContain("--hero-start:#aabbcc");
        cut.Markup.ShouldContain("--hero-end:#ddeeff");
    }

    [Fact]
    public void FeatureHomePage_hero_without_gradient_params_emits_no_hardcoded_colors()
    {
        IRenderedComponent<FeatureHomePage> cut = Render<FeatureHomePage>(ps => ps
            .Add(p => p.Title, "Operations")
            .Add(p => p.Icon, "home"));

        cut.Markup.ShouldNotContain("#1976d2");
        cut.Markup.ShouldNotContain("#42a5f5");
    }

    [Fact]
    public void KpiCard_without_color_falls_back_to_theme_token()
    {
        IRenderedComponent<KpiCard> cut = Render<KpiCard>(ps => ps
            .Add(p => p.Label, "Downloads")
            .Add(p => p.Value, "1,234"));

        cut.Markup.ShouldContain("var(--color-primary)");
        cut.Markup.ShouldNotContain("#4fc3f7");
    }

    [Fact]
    public void KpiCard_with_color_still_honours_explicit_value()
    {
        IRenderedComponent<KpiCard> cut = Render<KpiCard>(ps => ps
            .Add(p => p.Label, "Downloads")
            .Add(p => p.Value, "1,234")
            .Add(p => p.Color, "#ff8800"));

        cut.Markup.ShouldContain("#ff8800");
    }
}

/// <summary>
/// Guards that the Home breadcrumb href is configurable (so apps mounted under a path base
/// are not forced to the document-origin <c>"/"</c>), defaulting to <c>"/"</c>.
/// </summary>
public class BreadcrumbHomeHrefTests
{
    private sealed class PassthroughLocalizer<T> : IStringLocalizer<T>
    {
        public LocalizedString this[string name] => new(name, name, resourceNotFound: false);
        public LocalizedString this[string name, params object[] arguments] => new(name, name, resourceNotFound: false);
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => [];
    }

    private sealed class OneItemSource : IBreadcrumbSource
    {
        public IReadOnlyList<BreadcrumbDefinition> GetBreadcrumbs(string currentRoute) =>
            [new BreadcrumbDefinition("Settings", Href: "/settings")];
    }

    [Fact]
    public void Home_breadcrumb_defaults_to_root_href()
    {
        BreadcrumbService<BreadcrumbHomeHrefTests> service = new(
            new OneItemSource(),
            new PassthroughLocalizer<BreadcrumbHomeHrefTests>(),
            new BreadcrumbServiceOptions());

        IReadOnlyList<BreadcrumbItem> crumbs = service.GetBreadcrumbs("/settings");

        crumbs[0].Href.ShouldBe("/");
    }

    [Fact]
    public void Home_breadcrumb_honours_configured_HomeHref()
    {
        BreadcrumbService<BreadcrumbHomeHrefTests> service = new(
            new OneItemSource(),
            new PassthroughLocalizer<BreadcrumbHomeHrefTests>(),
            new BreadcrumbServiceOptions { HomeHref = "/app/" });

        IReadOnlyList<BreadcrumbItem> crumbs = service.GetBreadcrumbs("/settings");

        crumbs[0].Href.ShouldBe("/app/");
    }
}
