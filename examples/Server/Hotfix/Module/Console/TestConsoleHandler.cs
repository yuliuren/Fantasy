using Fantasy;
using Fantasy.Module.Console;

namespace Hotfix.Module.Console;

[ConsoleHandler(ConsoleMode.ReloadConfig)]
public class TestConsoleHandler : IConsoleHandler
{
    public async FTask Run(Scene scene, ModeContex contex, string content)
    {
        switch (content)
        {
            case ConsoleMode.ReloadConfig:
            {
                contex.Parent.RemoveComponent<ModeContex>();
                Log.Info("C must have config name, like: C UnitConfig");
                break;
            }
            
            default:
            {
                string[] ss = content.Split(" ");
                string configName = ss[1];
                string category = $"{configName}Category";
                Log.Info(category + "准备重载");
                break;
            }
        }
        
        await FTask.CompletedTask;
    }
}