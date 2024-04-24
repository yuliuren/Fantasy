using System.Net;
using Fantasy;
using Fantasy.Module.Http;

namespace Hotfix.Module.Http;

public static class HttpComponentSystem
{
    // public struct HttpAddressData
    // {
    //     public string address;
    // }

    public sealed class HttpComponentAwakeSystem : AwakeSystem<HttpComponent>
    {
        protected override void Awake(HttpComponent self)
        {
            string address = "http://*:20006/";
            try
            {
                self.Listener = new HttpListener();

                foreach (string s in address.Split(';'))
                {
                    if (s.Trim() == "")
                    {
                        continue;
                    }
                    self.Listener.Prefixes.Add(s);
                }

                self.Listener.Start();

                self.Accept().Coroutine();
            }
            catch (HttpListenerException e)
            {
                throw new Exception($"请先在cmd中运行: netsh http add urlacl url=http://*:你的address中的端口/ user=Everyone, address: {address}", e);
            }
        }
    }
    private static async FTask Accept(this HttpComponent self)
    {
        long instanceId = self.Id;
        while (self.Id == instanceId)
        {
            try
            {
                HttpListenerContext context = await self.Listener.GetContextAsync();
                self.Handle(context).Coroutine();
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
    
    private static async FTask Handle(this HttpComponent self, HttpListenerContext context)
    {
        try
        {
            IHttpHandler handler = HttpDispatcher.Instance.Get( context.Request.Url.AbsolutePath);
            await handler.Handle(self.Scene, context);
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
        context.Request.InputStream.Dispose();
        context.Response.OutputStream.Dispose();
    }

}