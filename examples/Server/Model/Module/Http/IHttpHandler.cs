using System.Net;

namespace Fantasy.Module.Http;

public interface IHttpHandler
{
    FTask Handle(Scene scene, HttpListenerContext context);
}