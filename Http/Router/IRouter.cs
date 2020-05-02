using System.Net;

namespace Http.Router
{
    public interface IRouter
    {
        public IRouter AddRequestHandler(string route, string method, IHttpHandler handler);
        public void HandleRequest(HttpListenerRequest request, HttpListenerResponse response);
    }
}