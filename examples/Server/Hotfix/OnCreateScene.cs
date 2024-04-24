﻿using Fantasy;
using Fantasy.Module.Console;
using Fantasy.Module.Http;

namespace Hotfix;

/// 当Scene创建时需要干什么
/// </summary>
public class OnCreateScene : AsyncEventSystem<Fantasy.OnCreateScene>
{
    public override async FTask Handler(Fantasy.OnCreateScene self)
    {
        // Fantasy服务器是以Scene为单位的、所以Scene下有什么组件都可以自己添加定义
        // OnCreateScene这个事件就是给开发者使用的
        // 比如Address协议这里、我就是做了一个管理Address地址的一个组件挂在到Address这个Scene下面了
        // 比如Map下你需要一些自定义组件、你也可以在这里操作
        var scene = self.Scene;



        switch (scene.SceneType)
        {
            case SceneType.Http:
            {
                // 挂载管理Address地址组件
                scene.AddComponent<HttpComponent>();
                break;
            }

        }

        scene.AddComponent<ConsoleComponent>();


        await FTask.CompletedTask;
    }
}