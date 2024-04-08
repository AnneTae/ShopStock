using System.Reflection;

namespace ShoopStock.Api.Installers;

public static class InstallerExtensions
{
    public static void InstallServicesInAssembly(
this IServiceCollection services,
IConfiguration configuration,
IWebHostEnvironment env)
    {
        var installers = Assembly.GetExecutingAssembly()
            .ExportedTypes
            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IInstaller>()
            .ToList();

        installers.ForEach(installer => installer.InstallServices(services, configuration, env));
    }
}
