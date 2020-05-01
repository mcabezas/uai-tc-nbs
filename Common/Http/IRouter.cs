using System.Net;

namespace Common.Http
{
    public interface IRouter
    {
        public IRouter AddRequestHandler(string route, string method, IHttpHandler handler);
        public void HandleRequest(HttpListenerRequest request, HttpListenerResponse response);
    }
}