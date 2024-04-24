using Fantasy.Module.Http;

namespace Hotfix.Module.Http;

using Fantasy;

public class HttpDispatcher : Singleton<HttpDispatcher>
{
    private readonly Dictionary<string, IHttpHandler> dispatcher = new();

    public override Task Load(long assemblyIdentity)
    {
        LoadHandler(assemblyIdentity);
        return Task.CompletedTask;
    }

    public override Task ReLoad(long assemblyIdentity)
    {
        LoadHandler(assemblyIdentity);
        return Task.CompletedTask;
    }

    private void LoadHandler(long assemblyIdentity)
    {
        HashSet<Type> types = new HashSet<Type>();
        foreach (var entitiesSystemType in AssemblySystem.ForEach(assemblyIdentity, typeof(IHttpHandler)))
        {
            types.Add(entitiesSystemType);
        }


        foreach (Type type in types)
        {
            object[] attrs = type.GetCustomAttributes(typeof(HttpHandlerAttribute), false);
            if (attrs.Length == 0)
            {
                continue;
            }

            HttpHandlerAttribute httpHandlerAttribute = (HttpHandlerAttribute)attrs[0];

            object obj = Activator.CreateInstance(type);

            IHttpHandler ihttpHandler = obj as IHttpHandler;
            if (ihttpHandler == null)
            {
                throw new Exception($"HttpHandler handler not inherit IHttpHandler class: {obj.GetType().FullName}");
            }

            if (this.dispatcher.ContainsKey(httpHandlerAttribute.Path))
            {
                throw new Exception($"HttpHandler handler not inherit IHttpHandler class: {obj.GetType().FullName}");
            }

            dispatcher.Add(httpHandlerAttribute.Path, ihttpHandler);
        }
    }

    public IHttpHandler Get(string path)
    {
        return this.dispatcher[path];
    }
}