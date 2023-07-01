using System;
using System.Linq;
using System.Collections.Generic;

public class ServiceLocator
{
    private static readonly Dictionary<Type, IService> services =
        new Dictionary<Type, IService>();

    private static readonly List<IService> servicesOrdered =
        new List<IService>();

    public static void Initialize()
    {
        foreach (var service in servicesOrdered)
        {
            service.OnInit();
        }
    }

    public static void Destroy()
    {
        foreach (var service in servicesOrdered.AsEnumerable().Reverse())
        {
            service.OnDestroy();
        }

        services.Clear();
        servicesOrdered.Clear();
    }

    /// <summary>
    /// The order you register services will affect the initialistion and
    /// destruction order. Initialisation will be the same as you registered,
    /// destruction will be the reverse of that order. This solves the dependency
    /// order.
    /// </summary>
    public static void RegisterService<T>(T service) where T : IService
    {
        services.Add(typeof(T), service);
        servicesOrdered.Add(service);
    }
    
    public static void RegisterService(Type type, IService service)
    {
        services.Add(type, service);
        servicesOrdered.Add(service);
    }

    public static T GetService<T>() where T : IService
    {
        return (T)services[typeof(T)];
    }
    }

