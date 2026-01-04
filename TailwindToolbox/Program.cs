using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using TailwindToolbox.Commands;
using TailwindToolbox.Services;

// Setup dependency injection
var services = new ServiceCollection();
services.AddSingleton<IProjectDetector, ProjectDetector>();
services.AddSingleton<IFileGenerator, FileGenerator>();
services.AddSingleton<INpmService, NpmService>();
services.AddSingleton<ITargetFileGenerator, TargetFileGenerator>();
services.AddSingleton<IValidationService, ValidationService>();

var registrar = new TypeRegistrar(services);
var app = new CommandApp(registrar);

app.Configure(config =>
{
    config.SetApplicationName("tailwind-blazor");

    // Register commands
    config.AddCommand<SetupCommand>("setup")
        .WithDescription("Initialize Tailwind CSS configuration in a Blazor project");

    config.AddCommand<CheckCommand>("check")
        .WithDescription("Validate Tailwind CSS configuration and dependencies");

    config.AddCommand<UpdateCommand>("update")
        .WithDescription("Update Tailwind CSS and related packages with breaking change detection");

    config.AddCommand<CreateTargetCommand>("create-target")
        .WithDescription("Create or update MSBuild .targets file for Tailwind CSS compilation");
});

return app.Run(args);

// Type registrar for dependency injection with Spectre.Console.Cli
internal sealed class TypeRegistrar : ITypeRegistrar
{
    private readonly IServiceCollection _services;

    public TypeRegistrar(IServiceCollection services)
    {
        _services = services;
    }

    public ITypeResolver Build()
    {
        return new TypeResolver(_services.BuildServiceProvider());
    }

    public void Register(Type service, Type implementation)
    {
        _services.AddSingleton(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        _services.AddSingleton(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> factory)
    {
        _services.AddSingleton(service, _ => factory());
    }
}

internal sealed class TypeResolver : ITypeResolver
{
    private readonly IServiceProvider _provider;

    public TypeResolver(IServiceProvider provider)
    {
        _provider = provider;
    }

    public object? Resolve(Type? type)
    {
        return type == null ? null : _provider.GetService(type);
    }
}
