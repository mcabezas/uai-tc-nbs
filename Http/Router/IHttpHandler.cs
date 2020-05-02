using System.Net;

namespace Http.Router
{
    public interface IHttpHandler
    {
        public void Handle(HttpListenerRequest request, HttpListenerResponse response);
    }
}