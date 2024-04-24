namespace Fantasy.Module.Console;

public static class ConsoleMode
{
    public const string ReloadDll = "R";
    public const string ReloadConfig = "C";
    public const string ShowMemory = "M";
    public const string Repl = "Repl";
    public const string Debugger = "Debugger";
    public const string CreateRobot = "CreateRobot";
    public const string Robot = "Robot";
}

public class ConsoleComponent: Entity
{
    public CancellationTokenSource CancellationTokenSource;

}