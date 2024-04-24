using Fantasy;
using Fantasy.Module.Console;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hotfix.Module.Console;

public static class ConsoleComponentSystem
{
    public class ConsoleComponentAwakeSystem : AwakeSystem<ConsoleComponent>
    {
        protected override void Awake(ConsoleComponent self)
        {
            self.Start().Coroutine();
        }
    }

    private static async FTask Start(this ConsoleComponent self)
    {
        self.CancellationTokenSource = new CancellationTokenSource();

        while (true)
        {
            try
            {
                ModeContex modeContex = self.GetComponent<ModeContex>();

                string line = await Task.Factory.StartNew(() =>
                {
                    System.Console.Write($"{modeContex?.Mode ?? ""}> ");
                    return System.Console.In.ReadLine();
                }, self.CancellationTokenSource.Token);

                line = line.Trim();

                switch (line)
                {
                    case "":
                        break;
                    case "exit":
                        self.RemoveComponent<ModeContex>();
                        break;
                    default:
                    {
                        string[] lines = line.Split(" ");
                        string mode = modeContex == null ? lines[0] : modeContex.Mode;

                        IConsoleHandler iConsoleHandler = ConsoleDispatcher.Instance.Get(mode);
                        if (modeContex == null)
                        {
                            modeContex = self.AddComponent<ModeContex>();
                            modeContex.Mode = mode;
                        }

                        if (iConsoleHandler != null)
                        {
                            await iConsoleHandler.Run(self.Scene, modeContex, line);
                            return;
                        }
                        Log.Warning($"Can Not Fint this command{line} ");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}