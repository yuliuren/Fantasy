using Fantasy;
using Fantasy.Module.Http;
using Hotfix.Module.Helper;

namespace Hotfix.Module.Http;

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

[HttpHandler("/get_router")]
public class HttpGetRouterHandler : IHttpHandler
{
    public async FTask Handle(Scene scene, HttpListenerContext context)
    {
        HttpHelper.Response(context, "response结果");
        await FTask.CompletedTask;
    }
}