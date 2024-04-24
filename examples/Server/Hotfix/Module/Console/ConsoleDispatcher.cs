using Fantasy;
using Fantasy.Module.Console;

namespace Hotfix.Module.Console;

public class ConsoleDispatcher:Singleton<ConsoleDispatcher>
{
    private readonly Dictionary<string, IConsoleHandler> handlers = new();
    
    
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
        foreach (var entitiesSystemType in AssemblySystem.ForEach(assemblyIdentity, typeof(IConsoleHandler)))
        {
            types.Add(entitiesSystemType);
        }

        foreach (Type type in types)
        {
            object[] attrs = type.GetCustomAttributes(typeof(ConsoleHandlerAttribute), false);
            if (attrs.Length == 0)
            {
                continue;
            }

            ConsoleHandlerAttribute consoleHandlerAttribute = (ConsoleHandlerAttribute)attrs[0];

            object obj = Activator.CreateInstance(type);

            IConsoleHandler iConsoleHandler = obj as IConsoleHandler;
            if (iConsoleHandler == null)
            {
                throw new Exception($"ConsoleHandler handler not inherit IConsoleHandler class: {obj.GetType().FullName}");
            }
            this.handlers.Add(consoleHandlerAttribute.Mode, iConsoleHandler);
        }
    }

    public IConsoleHandler Get(string key)
    {
        IConsoleHandler consoleHandler;
        this.handlers.TryGetValue(key, out consoleHandler);
        return consoleHandler;
    }
}