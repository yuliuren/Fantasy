using Fantasy.Base;

namespace Fantasy.Module.Http;

public class HttpHandlerAttribute: BaseAttribute
{
    public string Path { get; }

    public HttpHandlerAttribute(string path)
    {
        this.Path = path;
    }
}