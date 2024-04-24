using Fantasy;
using Fantasy.Module.Console;

namespace Hotfix.Module.Console;

public class ModeContexAwakeSystem : AwakeSystem<ModeContex>
{
    protected override void Awake(ModeContex self)
    {
        self.Mode = "";
    }
}

public class ModeContexDestroySystem : DestroySystem<ModeContex>
{
    protected override void Destroy(ModeContex self)
    {
        self.Mode = "";
    }
}