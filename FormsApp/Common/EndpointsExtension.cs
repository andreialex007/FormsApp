using System.Reflection;

namespace FormsApp.Common;

public static class EndpointsExtension
{
    public static WebApplication MapAllEndpoints(this WebApplication app)
    {
        var endPointType = typeof(IEndpoint);

        var assembly = Assembly.GetExecutingAssembly();

        var endpointTypes = assembly.GetExportedTypes()
            .Where(t => t.IsAbstract == false &&
                        t.GetInterfaces().Contains(endPointType))
            .ToList();

        Console.WriteLine($"Found {endpointTypes.Count} endpoint types");

        foreach (var type in endpointTypes)
        {
            Console.WriteLine($"Mapping endpoint: {type.Name}");
            if (Activator.CreateInstance(type) is IEndpoint instance)
                instance.Map(app);
        }

        return app;
    }
}