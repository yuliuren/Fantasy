namespace Fantasy.Module.Console;

public interface IConsoleHandler
{
    FTask Run(Scene scene, ModeContex contex, string content);
}