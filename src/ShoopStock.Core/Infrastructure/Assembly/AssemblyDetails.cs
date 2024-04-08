using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Security;

namespace ShoopStock.Core.Infrastructure.Assembly;

public class AssemblyDetails
{
    /// <summary>
    /// Get all types for all assemblies.
    /// </summary>
    /// <param name="namespaceStartsWith">Leave empty to load all types.</param>
    /// <returns> IEnumerable Type.</returns>
    public static IEnumerable<Type> FromAssembliesInSearchPath(string namespaceStartsWith = "")
    {
        return GetTypes(GetAssembliesInSearchPath(namespaceStartsWith));
    }

    /// <summary>
    /// If interface not exists register concrete type.
    /// </summary>
    /// <param name="type">.</param>
    /// <returns> GetNamedInterface. </returns>
    public static Type GetNamedInterface(Type type)
    {
        var namedInterface = type
            .GetInterfaces()
            .FirstOrDefault(i => i.Name.EndsWith(type.Name));

        if (namedInterface != null) return namedInterface;

        var interfaces = type.GetInterfaces();
        namedInterface = interfaces.Length > 0 ? interfaces[0] : type;

        return namedInterface;
    }

    private static IEnumerable<System.Reflection.Assembly> GetAssembliesInSearchPath(string namespaceStartsWith)
    {
        try
        {
            var assemblyNames = DependencyContext.Default!.GetDefaultAssemblyNames()
                .Where(a => a.Name!.StartsWith(namespaceStartsWith));

            var assemblies = assemblyNames
                .Select(LoadAssembly)
                .Where(x => x != null).ToList();

            return assemblies!;
        }
        catch (SecurityException)
        {
            return Array.Empty<System.Reflection.Assembly>();
        }
    }

    private static System.Reflection.Assembly? LoadAssembly(AssemblyName assemblyName)
    {
        try
        {
            var assembly = System.Reflection.Assembly.Load(assemblyName);
            return assembly;
        }
        catch (FileNotFoundException)
        {
            return null;
        }
        catch (FileLoadException)
        {
            return null;
        }
        catch (BadImageFormatException)
        {
            return null;
        }
    }

    private static IEnumerable<Type> GetTypes(IEnumerable<System.Reflection.Assembly> assemblies)
    {
        return assemblies.SelectMany(assembly =>
        {
            try
            {
                return GetTypes(assembly.DefinedTypes);
            }
            catch (ReflectionTypeLoadException ex)
            {
                return GetTypes(ex.Types
                    .TakeWhile(x => x != null)
                    .Select(x => x!.GetTypeInfo()));
            }
        });
    }

    private static IEnumerable<Type> GetTypes(IEnumerable<TypeInfo> typeInfos)
    {
        return typeInfos
            .Where(x => x.IsClass && !x.IsAbstract && !x.IsValueType && x.IsVisible)
            .Select(ti => ti.AsType());
    }
}
